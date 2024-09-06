using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrintMaster.Application.Handle.HandleEmail;
using PrintMaster.Application.InterfaceServices;
using PrintMaster.Application.Payloads.Mappers;
using PrintMaster.Application.Payloads.RequestModels.UserRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataLogin;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;
using PrintMaster.Application.Payloads.Responses;
using PrintMaster.Commons.Utils;
using PrintMaster.Domain.Entities;
using PrintMaster.Domain.InterfaceRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrintMaster.Application.ImplementServices
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Role> _baseRoleRepository;
        private readonly UserConverter _converter;
        private readonly IBaseRepository<Team> _baseTeamRepository;
        private readonly IBaseRepository<Permission> _basePermissionRepository;
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<RefreshToken> _baseRefreshTokenRepository;
        private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
        private readonly IEmailService _emailService;

        public AuthService(IBaseRepository<User> baseUserRepository,
                           IUserRepository userRepository,
                           IBaseRepository<Role> baseRoleRepository,
                           UserConverter converter,
                           IBaseRepository<Team> baseTeamRepository,
                           IBaseRepository<Permission> basePermissionRepository,
                           IConfiguration configuration,
                           IBaseRepository<RefreshToken> baseRefreshTokenRepository,
                           IBaseRepository<ConfirmEmail> baseConfirmEmailRepository,
                           IEmailService emailService)
        {
            _baseUserRepository = baseUserRepository;
            _userRepository = userRepository;
            _baseRoleRepository = baseRoleRepository;
            _converter = converter;
            _baseTeamRepository = baseTeamRepository;
            _basePermissionRepository = basePermissionRepository;
            _configuration = configuration;
            _baseRefreshTokenRepository = baseRefreshTokenRepository;
            _baseConfirmEmailRepository = baseConfirmEmailRepository;
            _emailService = emailService;
        }

        
        public async Task<ResponseMessages> ChangePassword(Guid userId, Request_ChangePassword request)
        {
            try
            {
                var user = await _baseUserRepository.GetByIDAsync(userId);
                bool checkPassword = SecurityUtilities.IsPasswordValid(request.OldPassword, user.Password);
                if (!checkPassword)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác"
                    };
                }
                if (!request.NewPassword.Equals(request.ConfirmPassword))
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không trùng khớp"
                    };
                }

                user.Password = SecurityUtilities.HashPassword(request.NewPassword);
                await _baseUserRepository.UpdateAsync(user);
                return new ResponseMessages
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đổi mật khẩu thành công"
                };

            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Có lỗi : {ex.Message}"
                };
            }
        }

        public async Task<ResponseMessages> ConfirmCreateNewPassword(Request_ConfirmCreateNewPassword request)
        {
            try
            {
                var confirmEmail = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(request.ConfirmCode));
                if (confirmEmail == null)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mã xác nhận không hợp lệ"
                    };
                }
                if (confirmEmail.ExpiryTime < DateTime.Now)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mã xác nhận đã hết hạn"
                    };
                }
                var user = await _baseUserRepository.GetByIDAsync(confirmEmail.UserId);
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không trùng khớp"
                    };
                }
                user.Password = SecurityUtilities.HashPassword(request.Password);
                await _baseUserRepository.UpdateAsync(user);
                confirmEmail.IsConfirm = true;
                await _baseConfirmEmailRepository.UpdateAsync(confirmEmail);
                return new ResponseMessages
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo mật khẩu mới thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Có lỗi : {ex.Message}"
                };
            }
        }

        public async Task<ResponseMessages> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Người dùng không tồn tại"
                    };
                }
                await _baseConfirmEmailRepository.DeleteAsync(x => x.UserId == user.Id);
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = StringUltilities.GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(3),
                    Id = Guid.NewGuid(),
                    IsConfirm = false,
                    UserId = user.Id
                };
                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);

                var message = new EmailMessage(new string[] { email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận: {confirmEmail.ConfirmCode}");
                var responseMessage = _emailService.SendEmail(message);

                return new ResponseMessages
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Mã xác nhận đã được gửi về email của bạn! Vui lòng check Email"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Có lỗi : {ex.Message}"
                };
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user)
        {
            var permissions = await _basePermissionRepository.GetAllAsync(x => x.UserId == user.Id);
            var roles = await _baseRoleRepository.GetAllAsync();

            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Avatar", user.Avatar),
                new Claim("FullName", user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var permission in permissions)
            {
                foreach (var role in roles)
                {
                    if (role.Id == permission.RoleId)
                    {
                        authClaims.Add(new Claim("Permission", role.RoleName));
                    }
                }
            }

            var userRoles = await _userRepository.GetRolesOfUserAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var refreshToken = StringUltilities.GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            RefreshToken rf = new RefreshToken
            {
                UserId = user.Id,
                ExpiryTime = DateTime.UtcNow.AddHours(refreshTokenValidity),
                Token = refreshToken
            };

            rf = await _baseRefreshTokenRepository.CreateAsync(rf);

            return new ResponseObject<DataResponseLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Token created successfully",
                Data = new DataResponseLogin
                {
                    Id = rf.Id,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    RefreshToken = refreshToken,
                    User = new DataResponseUser
                    {
                        CreateTime = user.CreateTime,
                        DateOfBirth = user.DateOfBirth,
                        Email = user.Email,
                        FullName = user.FullName,
                        Id = user.Id,
                        PhoneNumber = user.PhoneNumber,
                    }
                }
            };
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            try
            {
                var user = await _userRepository.GetUserByUsername(request.Username);
                if (user == null)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Tài khoản không chính xác",
                        Data = null
                    };
                }
                var checkPassword = SecurityUtilities.IsPasswordValid(request.Password, user.Password);
                if (!checkPassword)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác",
                        Data = null
                    };
                }
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đăng nhập thành công",
                    Data = new DataResponseLogin
                    {
                        Id = user.Id,
                        AccessToken = GetJwtTokenAsync(user).Result.Data.AccessToken,
                        RefreshToken = GetJwtTokenAsync(user).Result.Data.RefreshToken,
                        User = new DataResponseUser
                        {
                            CreateTime = user.CreateTime,
                            DateOfBirth = user.DateOfBirth,
                            Email = user.Email,
                            FullName = user.FullName,
                            Id = user.Id,
                            PhoneNumber = user.PhoneNumber,
                        }
                    }
                };
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser>> Register(Request_Register request)
        {
            try
            {
                if (await _userRepository.GetUserByEmail(request.Email) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email đã tồn tại trên hệ thống",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByPhone(request.PhoneNumber) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Số điện thoại đã tồn tại trên hệ thống",
                        Data = null
                    };
                }
                if (await _userRepository.GetUserByUsername(request.Username) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Tên tài khoản đã tồn tại",
                        Data = null
                    };
                }
                if (!ValidationUtilities.IsEmailValid(request.Email))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Định dạng email không hợp lệ",
                        Data = null
                    };
                }
                if (!ValidationUtilities.IsPhoneNumberValid(request.PhoneNumber))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Định dạng số điện thoại không hợp lệ",
                        Data = null
                    };
                }
                var user = new User
                {
                    Avatar = "https://i.pngimg.me/thumb/f/720/a43893ada7.jpg",
                    IsDeleted = false,
                    CreateTime = DateTime.Now,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    FullName = request.FullName,
                    Id = Guid.NewGuid(),
                    Gender = request.Gender,
                    Password = SecurityUtilities.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    UserName = request.Username,
                    TeamId = request.TeamId,
                    Status = Commons.Enumerates.UserStatus.UnActivated,
                };
                user = await _baseUserRepository.CreateAsync(user);
                await _userRepository.AddRolesToUser(user, new List<string> { "Employee" });

                var team = await _baseTeamRepository.GetAsync(x => x.Id == user.TeamId);
                team.NumberOfMember = await _baseTeamRepository.CountAsync(x => x.Id == user.TeamId);
                await _baseTeamRepository.UpdateAsync(team);

                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    Id = Guid.NewGuid(),
                    ConfirmCode = StringUltilities.GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(5),
                    IsConfirm = false,
                    UserId = user.Id,
                };

                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);

                var message = new EmailMessage(new string[] { request.Email }, "Nhận mã xác nhận tại đây", $"Mã xác nhận : {confirmEmail.ConfirmCode}");
                var responseMessage = _emailService.SendEmail(message);


                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Bạn gửi yêu cầu đăng kí thành công, lấy mã xác nhận ở email",
                    Data = new DataResponseUser
                    {
                        Id = user.Id,
                        CreateTime = user.CreateTime,
                        DateOfBirth = user.DateOfBirth,
                        Email = user.Email,
                        Avatar = user.Avatar,
                        Gender = user.Gender.ToString(),
                        TeamName = _baseTeamRepository.GetAsync(x => x.Id == user.TeamId).Result.Name,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Status = user.Status.ToString(),
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseMessages> ConfirmRegister(string confirmCode)
        {
            try
            {
                var code = await _baseConfirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(confirmCode));
                if (code == null)
                {
                    return new ResponseMessages { Status = StatusCodes.Status400BadRequest, Message = "Mã xác nhận không hợp lệ" };
                }

                var user = await _baseUserRepository.GetAsync(x => x.Id == code.UserId);

                if (code.ExpiryTime < DateTime.Now)
                {
                    return new ResponseMessages
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mã xác nhận đã hết hạn"
                    };
                }

                user.Status = Commons.Enumerates.UserStatus.Activated;
                code.IsConfirm = true;

                await _baseUserRepository.UpdateAsync(user);
                await _baseConfirmEmailRepository.UpdateAsync(code);


                return new ResponseMessages
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Kích hoạt tài khoản thành công!!!"
                };

            }
            catch (Exception ex)
            {
                return new ResponseMessages
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Có lỗi : {ex.Message}"
                };
            }
        }

        #region Private Method
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expirationTimeUtc = DateTime.UtcNow.AddHours(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;

        }
        #endregion
    }
}
