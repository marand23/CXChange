using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserVersionShouldMatchOnUpdate : IBussinessRuleSpecification
    {
        string databaseVersionStamp;
        string versionToUpdateStamp;
        public UserVersionShouldMatchOnUpdate(string databaseVersionStamp, string versionToUpdateStamp)
        {
            this.databaseVersionStamp = databaseVersionStamp;
            this.versionToUpdateStamp = versionToUpdateStamp;
        }

        public string Message => "User Doamin Concurrency Conflict";

        public Enum Code => BusinessRuleErrorEnum.UserUpdateVersionMismatch;

        public bool isSatisfiedBy()
        {
            return databaseVersionStamp.Equals(versionToUpdateStamp);
        }
    }
}
