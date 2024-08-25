using PrintMaster.Application.InterfaceServices;

namespace PrintMaster.Api.MiddleWare
{
    public class ValidateJwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ValidateJwtMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var blacklistedTokenService = scope.ServiceProvider.GetRequiredService<IBlacklistedTokenService>();

                    if (blacklistedTokenService.IsTokenBlacklisted(token))
                    {
                        context.Response.StatusCode = 401; // Unauthorized
                        await context.Response.WriteAsync("Token không hợp lệ.");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
