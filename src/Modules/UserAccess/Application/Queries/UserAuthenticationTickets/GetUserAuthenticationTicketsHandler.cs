using CXChange.BuildingBlocks.Application.Query;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Queries.UserAuthenticationTickets
{
    internal class GetUserAuthenticationTicketsHandler : IConsumer<GetUserAuthenticationTickets>
    {
        private readonly IQueryHandler<GetUserAuthenticationTickets, TicketsResult> _queryHandler;
        public GetUserAuthenticationTicketsHandler(IQueryHandler<GetUserAuthenticationTickets, TicketsResult> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task Consume(ConsumeContext<GetUserAuthenticationTickets> context)
        {
            var initalQuery = $"select deviceid from xchangetest.`useraccess.authenticationtickets`  {SqlKeyWords.Where} UserId=@UserId group by deviceid ";
            _queryHandler.AddToParameters("@UserId", "2e58119f-4d31-43cc-b0e5-58bb90d1c8f6");
            var result = await _queryHandler.ExecuteWithPaginationAndOrderingAsync(context.Message, initalQuery);
            await context.RespondAsync(new TicketList { Result = result });
        }
    }
}
