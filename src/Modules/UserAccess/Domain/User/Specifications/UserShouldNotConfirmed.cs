using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UsersPhoneNumberShouldNotBeConfirmed : IBussinessRuleSpecification
    {
        private bool PhoneNumberConfirmed;
        public UsersPhoneNumberShouldNotBeConfirmed(bool PhoneNumberConfirmed)
        {
            this.PhoneNumberConfirmed = PhoneNumberConfirmed;
        }
        public string Message => "User already exist";

        public Enum Code => BusinessRuleErrorEnum.UserAlreadyExist;

        public bool isSatisfiedBy()
        {
            return !PhoneNumberConfirmed;
        }
    }
}
