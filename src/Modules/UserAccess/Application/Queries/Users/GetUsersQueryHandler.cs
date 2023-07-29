using CXChange.BuildingBlocks.Application.Data;
using CXChange.BuildingBlocks.Application.Query;
using Dapper;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    internal class GetUsersQueryHandler : IConsumer<GetUsers>
    {
        private ISqlConnectionFactory _connectionFactory;
        public GetUsersQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Consume(ConsumeContext<GetUsers> context)
        {
            var connection = _connectionFactory.GetOpenConnection();
            string sql =
                "select * from   UserManagement.AspNetUsers order by id ";
            var sql2 = PagedQuery.ToSqlQuery(context.Message, sql);
            IEnumerable<UserDto> users = await connection.QueryAsync<UserDto>(sql2.RawSql, sql2.Parameters);
            var res = new UserList
            {
                users = users.ToList()






            };
            context.RespondAsync(res);
        }
    }
}

