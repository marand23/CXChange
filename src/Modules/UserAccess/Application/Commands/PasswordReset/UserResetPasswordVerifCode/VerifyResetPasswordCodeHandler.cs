using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode
{
    internal class VerifyResetPasswordCodeHandler : IConsumer<VerifyResetPasswordCode>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _verifier;
        ITicketRepository _ticket;
        public VerifyResetPasswordCodeHandler(IUserRepository repo, IVerificationCodeFunctions verifier, ITicketRepository ticket)
        {
            _repo = repo;
            _verifier = verifier;
            _ticket = ticket;
        }

        public async Task Consume(ConsumeContext<VerifyResetPasswordCode> context)
        {
            await _ticket.RemoveAllAsync("fs");

            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new UserAccessCommandException(Enums.CommandExceptionEnum.UserNotFound);
            }
            var snapshot = user.GetSnapShot();
            var result = _verifier.Verify(user, int.Parse(context.Message.VerificationCode), CodeType.GeneratePurposeForSpecificTwoFactor(snapshot.Id, KeyWords.ChangePassword, snapshot.PhoneNumber));
            if (!result)
            {
                throw new UserAccessCommandException(Enums.CommandExceptionEnum.VerificationCodeInvalid);
            }
            var token = await _repo.GeneratePasswordResetToken(user);

            await context.RespondAsync(new ResetPasswordCodeVerified { Token = token });

        }
    }
}
