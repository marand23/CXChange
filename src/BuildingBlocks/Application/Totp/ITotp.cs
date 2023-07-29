namespace CXChange.BuildingBlocks.Application.Totp
{
    public interface ITotp
    {
        int GenerateCode(string securityStamp, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6);
        bool ValidateCode(byte[] securityToken, int code, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6);
    }
}
