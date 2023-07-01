using CXChange.CoreElements.Application.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Application.User.Queries.Users.GetUsers
{
    internal class GetUsersQuery:IRequest,IPagination
    {
        public GetUsersQuery(int page,int pageSize)
        {
            Page = page;
            this.pageSize = pageSize;
        }

        public int Page { get; set; }

        public int page { get; }
        public int pageSize { get; }
    }
}
