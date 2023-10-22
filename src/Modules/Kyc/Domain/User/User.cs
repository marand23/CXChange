using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.User.ValueObjects;

namespace CXChange.Modules.Kyc.Domain.User
{
    public class User : Entity, IAggregateRoot
    {
        private User(string id)
        {
            Id = id;
        }
        private string Id { get; set; }
        private FullName FullName { get; set; }
        private NationalId NationalId { get; set; }
        private DateOnly BirthDate { get; set; }
        public static User CreateUser(string id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }
            return new User(id);

        }
    }

};
