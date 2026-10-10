using CXChange.Modules.UserAccess.Administrator.Application.Queries.Users;
using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Queries.Users
{
    public class GetUsersValidator : AbstractValidator<GetUsers>

    {
        public List<string> allowedOrderBy = new List<string>()
        {
            "PhoneNumber",
            "PhoneNumberConfirmed"
        };
        public GetUsersValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            //RuleFor(x => x.OrderBy).Null().When(x => x.OrderBy != null);
            //RuleFor(x => x.OrderBy).Must(orderBy => orderBy.All(order => allowedOrderBy.Contains(order.Field))).
            //    WithErrorCode("OrderByValidator");
            //  RuleFor(x => x.Filters.PhoneNumber.Value).PhoneNumber().WithName("PhoneNumber");
            //RuleFor(x => x.Page).Page();
            //RuleFor(x => x.PageSize).PageSize();

        }
    }

}
