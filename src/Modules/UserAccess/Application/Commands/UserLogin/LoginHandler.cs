using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    public class LoginHandler : IConsumer<LoginUser>
    {
        public async Task Consume(ConsumeContext<LoginUser> context)
        {
            await context.RespondAsync(new UserLogined {  });
        }
    }
}
