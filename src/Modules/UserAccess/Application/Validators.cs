using FluentValidation;

namespace CXChange.Modules.UserAccess.Application
{
    public static class Validators
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().Matches("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}").WithErrorCode("PhoneNumberValidator");


        }
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmptyNotNull().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$").WithErrorCode("PasswordValidator");
        }
        public static IRuleBuilderOptions<T, string> VerificationCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmptyNotNull().Matches("^[0-9]{6}$").WithErrorCode("VerificationCodeValidator");
        }
        public static IRuleBuilderOptions<T, string> UserToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }
        public static IRuleBuilderOptions<T, Guid> UserId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }
        public static IRuleBuilderOptions<T, string> TwoFactorType<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().Must(x =>
            {
                return (x == "SMS" || x == "Email");
            });
        }
        public static IRuleBuilderOptions<T, string> NotEmptyNotNull<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().WithErrorCode("NotEmptyNotNullValidator");


        }
        public static IRuleBuilderOptions<T, int> PageSize<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(x => x > 0).WithErrorCode("PageSizeValidator");
        }
        public static IRuleBuilderOptions<T, int> Page<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(x => x > 0).WithErrorCode("PageValidator");
        }


    }
}
