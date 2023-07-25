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
using MassTransit.Caching;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using MediatR;
using FluentValidation;
using CXChange.BuildingBlocks.Application;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public class RegisterUserHandler :IConsumer<RegisterUser>
    {
        IUserRepository _userRepository;
        IDomainEventDispatcher _dispatcher;
       
        public RegisterUserHandler(IUserRepository userRepository,IDomainEventDispatcher dispatcher)
        {
            _userRepository = userRepository;
            _dispatcher = dispatcher;
         
           
        }

        public async Task Consume(ConsumeContext<RegisterUser> context)
        {


            //var user = await _userRepository.GetUserByPhoneNumber(context.Message.PhoneNumber);
            //if (user != null)
            //{
            //    user.CheckRegisterPossiblity();

            //}
           var user= new User();
           var a = user.RegisterNewUser(context.Message.PhoneNumber, context.Message.Password);
             await _userRepository.CreateUser(a);
           
            await _dispatcher.Dispatch(user.DomainEvents);






            await context.RespondAsync(new UserRegistered { PhoneNumber=context.Message.PhoneNumber,TwoFactorType=TwoFactorType.SMS});
            {
                

    
             };
            

        }

     
           
        }





        //protected async override  Task<UserRegistered> Handle(RegisterUser request, CancellationToken cancellationToken)

    }

