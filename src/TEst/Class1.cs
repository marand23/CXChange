using CXChange.Modules.UserAccess.IntegrationEvents;
using MassTransit;
using System.Drawing;

namespace TEst
{
    public class Class1
    {

    }
    public class UserRegisteredConsumer : IConsumer<UserRegistrationConfirmed>
    {
        public Task Consume(ConsumeContext<UserRegistrationConfirmed> context)
        {
            throw new NotImplementedException();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"Recieved Event userid={context.Message.Id}, phoneNumber={context.Message.PhoneNumber}");
            Console.ResetColor();

            return Task.CompletedTask;
        }


    }
    public class secondconsumer : IConsumer<UserRegistrationConfirmed>
    {
        public Task Consume(ConsumeContext<UserRegistrationConfirmed> context)
        {
           // throw new Exception();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"second consumer userid={context.Message.Id}, phoneNumber={context.Message.PhoneNumber}");
            Console.ResetColor();

            return Task.CompletedTask;
        }


    }
    public class DashboardFaultConsumer :
    IConsumer<Fault<UserRegistrationConfirmed>>
    {
        public async Task Consume(ConsumeContext<Fault<UserRegistrationConfirmed>> context)
        {
          Console.WriteLine(context.Message);
        }
    }


    public class CheckOrderStatusConsumer :
    IConsumer<CheckOrderStatus>
    {
       

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {


            await context.RespondAsync<OrderStatusResult>(new
            {
                OrderId = "4848",
                 Timestamp = DateTime.UtcNow,
                
                   
                 
            }); ;
        }

    }
   
}