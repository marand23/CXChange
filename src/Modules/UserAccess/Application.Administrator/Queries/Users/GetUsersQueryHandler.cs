using CXChange.BuildingBlocks.Application.Query;
using CXChange.Modules.UserAccess.Administrator.Application.Queries.Users;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    internal class GetUsersQueryHandler : IConsumer<GetUsers>
    {

        private readonly IQueryHandler<GetUsers, UserDto> _queryHandler;
        public GetUsersQueryHandler(IQueryHandler<GetUsers, UserDto> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task Consume(ConsumeContext<GetUsers> context)
        {

            if (context.Message.Filters.PhoneNumber != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.PhoneNumber, nameof(context.Message.Filters.PhoneNumber));
            }
            if (context.Message.Filters.PhoneNumberConfirmed != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.PhoneNumberConfirmed, nameof(context.Message.Filters.PhoneNumberConfirmed));
            }

            if (context.Message.Filters.EmailConfirmed != null)
            {
                _queryHandler.BuildFilterSql(context.Message.Filters.EmailConfirmed, "EmailConfirmed");
            }

            var initalQuery = $@"
    SELECT {nameof(UserDto.Id).ToSnakeCase()}, 
           {nameof(UserDto.PhoneNumber).ToSnakeCase()}, 
           {nameof(UserDto.UserName).ToSnakeCase()}
    FROM   {"user_access.asp_net_users".ToSnakeCase()}";


            _queryHandler.AddToParameters("id", "this is id");
            var result = await _queryHandler.ExecuteWithPaginationAndOrderingAsync(context.Message, initalQuery);
            if (result == null) { throw new ArgumentNullException("query did not return any results"); }
            await context.RespondAsync(new UserList { users = result });

        }


    }
}