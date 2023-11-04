using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Exceptions;
using CXChange.Modules.UserAccess.Domain;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordCofirm
{
    internal class ResetUserPasswordHandler : IConsumer<ResetUserPasswordConfirm>
    {
        IUserRepository _repo;
        IDomainEventDispatcher _dispatcher;


        public ResetUserPasswordHandler(IUserRepository repo, IDomainEventDispatcher dispatcher, IMediator mediator, ITicketRepository ticket, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _dispatcher = dispatcher;

        }
        public async Task Consume(ConsumeContext<ResetUserPasswordConfirm> context)
        {

            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new UserAccessCommandException(CommandExceptionEnum.UserNotFound);
            }


            user.CheckUserPasswordReset();
            ;
            await _repo.ResetPassword(user, context.Message.Token, context.Message.NewPassword);
            await _dispatcher.Dispatch(user.DomainEvents);
            user.UpdateSecurityStamp();
            await _repo.Update(user);

            await context.RespondAsync(new UserPasswordReset { });



        }
    }
}
