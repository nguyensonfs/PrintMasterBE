namespace PrintMaster.Application.InterfaceServices
{
    public interface IBlacklistedTokenService
    {
        void BlacklistToken(string token, TimeSpan expiration);
        bool IsTokenBlacklisted(string token);
    }
}
