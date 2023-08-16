namespace CXChange.BuildingBlocks.Application.Totp
{
    public interface ITotp
    {
        int GenerateCode(string securityStamp, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6);
        bool ValidateCode(string securityStamp, int code, TimeSpan _timestep, string modifier = null, int numberOfDigits = 6);
    }
}
