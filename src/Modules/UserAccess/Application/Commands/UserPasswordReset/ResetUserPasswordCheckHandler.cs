using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Commands.UserPasswordReset;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordChangeCheck
{
    internal class ResetUserPasswordCheckHandler : IConsumer<CheckUserPasswordReset>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;
        public ResetUserPasswordCheckHandler(IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _repo = repo;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<CheckUserPasswordReset> context)
        {
            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user does not exist", (int)CommandExceptionEnum.UserDoesNotExist);


            }
            var token = await _repo.GeneratePasswordResetToken(user);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new PasswordResetChecked { PhoneNumber = context.Message.PhoneNumber, Token = token });
        }
    }
}

