using CXChange.BuildingBlocks.Application.Query;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Queries.UserGet
{
    internal class GetUserHandler : IConsumer<GetUser>
    {
        private readonly IQueryHandler<GetUser, Result> _queryHandler;
        public GetUserHandler(IQueryHandler<GetUser, Result> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task Consume(ConsumeContext<GetUser> context)
        {
            //var result = await _queryHandler.FirstOrDefaultAsync(context.Message,
            //    $" select " +
            //    $" PhoneNumber as {nameof(Result.PhoneNumber)} ," +
            //    $"PhoneNumberConfirmed as  {nameof(Result.IsPhoneConfirmed)}, " +
            //    $"EmailConfirmed as {nameof(Result.IsEmailConfirmed)}, " +
            //    $"IsAuthenticatorEnabled as {nameof(Result.IsTotpConfirmed)} " +



            //    $" from xchangetest.`useraccess.aspnetusers`");
            //if (result == null) { throw new ArgumentNullException("query did not return any results"); }
            //await context.RespondAsync(result);
        }


    }
}
