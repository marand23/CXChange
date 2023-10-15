using CXChange.BuildingBlocks.Application.Query;
using Dapper;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    internal class GetUsersQueryHandler : IConsumer<GetUsers>
    {
        SqlBuilder builder = new SqlBuilder();
        private readonly QueryHandler<GetUsers, UserDto> _queryHandler;
        public GetUsersQueryHandler(QueryHandler<GetUsers, UserDto> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task Consume(ConsumeContext<GetUsers> context)
        {

            var result = await _queryHandler.ExecuteAsync(context.Message,
                $" Select * " +

                $"  from xchangetest.`useraccess.aspnetusers` ");
            if (result == null) { throw new ArgumentNullException("query did not return any results"); }
            await context.RespondAsync(new UserList { users = result });
        }
    }
}
