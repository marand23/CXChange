namespace CXChange.BuildingBlocks.Application.Totp
{
    public interface IAuthenticator
    {
        bool ValidateTwoFactorPIN(string secret, string twoFactorCodeFromClient, TimeSpan timeTolerance);
        string GenerateAuthenticatorUrl(string secretkey, string issuer, string phone);
    }
}
