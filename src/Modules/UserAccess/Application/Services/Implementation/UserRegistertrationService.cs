using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CXChange.Modules.UserAccess.Application.RequestModels.UserRegistration;
using CXChange.Modules.UserAccess.Domain.User.Exceptions;
using CXChange.CoreElements.Application.Totp;
using MassTransit;
using CXChange.Modules.UserAccess.IntegrationEvents;
using System.Threading;
using Microsoft.AspNetCore.Components.Forms;

namespace CXChange.Modules.UserAccess.Application.Services.Implementation
{
    internal class UserRegistertrationService:IUserRegistrationService
    {
        
        private readonly IUserRepository _userRepository;
        private readonly ITotp _totp;
        private readonly IPublishEndpoint _publishEndpoint;
        IRequestClient<CheckOrderStatus> _client;

        public UserRegistertrationService(IUserRepository userRepository,ITotp totp,IPublishEndpoint publishEndpoint, IRequestClient<CheckOrderStatus> client)
        {
           
            _userRepository = userRepository;
            _totp = totp; 
            _publishEndpoint = publishEndpoint;
            _client = client;
        }
        public async Task<string> RegisterUser(Register credentials)
        {
            try
            {
                var response = await _client.GetResponse<OrderStatusResult>(new { orderId="123" });

                //return response.Message.OrderId + response.Message.Timestamp.ToString();
               
          
                //var user = await _userRepository.GetUserByPhoneNumber(credentials.PhoneNumber);
                //if (user != null)
                //{
                //    user.CheckRegisterPossiblity();

                //    return "registered";

                //}
                await _publishEndpoint.Publish(new UserRegistrationConfirmed { Id = "fsdfs", PhoneNumber = credentials.PhoneNumber });
                await _userRepository.CreateUser(new Domain.User.User(credentials.PhoneNumber,credentials.Password,true,"SMS"),credentials.Password);
                //  byte[] securityToken =await _userRepository.GetUserSecurityToken(user);
                //   int code = _totp.GenerateCode(securityToken,TimeSpan.FromMinutes(3),"CreateUser");
  
                return "registered";
            }
            catch (UserRegistrationException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
         

        }
        public async Task ConfirmRegistration(RegisterConfirmation confirmation)
        {
            try
            {
                var user = await _userRepository.GetUserByPhoneNumber(confirmation.PhoneNumber);
                if (user != null)
                {
                    user.CheckRegisterPossiblity();
                    byte[] securityToken = await _userRepository.GetUserSecurityToken(user);
                    var result= _totp.ValidateCode(securityToken,int.Parse(confirmation.code),TimeSpan.FromMinutes(3),"CreateUser");
                    if (result)
                    {
                        user.ConfirmPhoneNumber();
                        await _userRepository.UpdateUser(user);
                    }


                }
               
            

            }
            catch (UserRegistrationException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }
    }

}
