using CXChange.CoreElements.Application.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Application.User.Queries.Users.GetUsers
{
    internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory) { 
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public Task Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}
