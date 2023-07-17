using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.MessageHeaders;
using MassTransit.Caching;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MediatR;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public class RegisterUserHandler :IRequestHandler<RegisterUser,UserRegistered>
    {
        IUserRepository _userRepository;
        IDomainEventDispatcher _dispatcher;
        public RegisterUserHandler(IUserRepository userRepository,IDomainEventDispatcher dispatcher)
        {
            _userRepository = userRepository;
            _dispatcher = dispatcher;
           
        }

       

       


       public async Task<UserRegistered> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            
            var user = await _userRepository.GetUserByPhoneNumber("1234");
            user.GetSnapShot();
            
            // var user = await _userRepository.GetUserByPhoneNumber(request.PhoneNumber);
            //if (user != null)
            //{
            //    user.CheckRegisterPossiblity();
            //    return new UserRegistered { PhoneNumber = request.PhoneNumber };

            //}
            //  await _publishEndpoint.Publish(new UserRegistrationConfirmed { Id = "fsdfs", PhoneNumber = credentials.PhoneNumber });
            await _userRepository.CreateUser(new Domain.User.User(request.PhoneNumber, request.Password, true, "SMS"), request.Password);
            //  byte[] securityToken =await _userRepository.GetUserSecurityToken(user);
            //   int code = _totp.GenerateCode(securityToken,TimeSpan.FromMinutes(3),"CreateUser");
           await _dispatcher.Dispatch(user.DomainEvents);
            return new UserRegistered { PhoneNumber = request.PhoneNumber };
        }





        //protected async override  Task<UserRegistered> Handle(RegisterUser request, CancellationToken cancellationToken)

    }
}
