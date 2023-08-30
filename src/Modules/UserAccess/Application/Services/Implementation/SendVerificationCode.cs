using CXChange.BuildingBlocks.Application.Totp;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utilis;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Services.Implementation
{
    internal class VerificationCodeFunctions : IVerificationCodeFunctions
    {
        ITotp _totp;
        IBus _bus;
        public VerificationCodeFunctions(ITotp totp, IBus bus) { _totp = totp; _bus = bus; }
        public async Task Send(User user, string modifier)
        {

            var snapShot = user.GetSnapShot();
            if (snapShot.DefaultTwoFactor == "Authenticator")
            {
                await Task.CompletedTask;
                return;
            }
            string verificationCode = _totp.GenerateCode(snapShot.SecurityStamp, DateTimeConstants.TotpTimeSpan, modifier).ToString();

            await _bus.Publish(new UserAccessSendVerificationIntegrationEvent
            {

                Destination = snapShot.DefaultTwoFactor == "SMS" ? snapShot.PhoneNumber : snapShot.Email,
                VerificationCode = verificationCode,
                DestinationType = snapShot.DefaultTwoFactor == "SMS" ? DestinationType.Phone : DestinationType.Email
            });
            Console.WriteLine("code sended:" + verificationCode);

        }

        public bool Verify(User user, int code, string modifier)
        {
            var snapshot = user.GetSnapShot();
            bool result = _totp.ValidateCode(snapshot.SecurityStamp, code, DateTimeConstants.TotpTimeSpan, modifier);
            Console.WriteLine("code verifies");
            return result;
        }
    }
}
