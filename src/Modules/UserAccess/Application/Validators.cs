using FluentValidation;

namespace CXChange.Modules.UserAccess.Application
{
    public static class Validators
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().Length(11).Matches("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}");


        }
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }
        public static IRuleBuilderOptions<T, string> VerificationCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().Length(6).Matches("^ [0-9] {6}$");
        }
        public static IRuleBuilderOptions<T, string> UserToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty();
        }
        public static IRuleBuilderOptions<T, string> UserId<T>(this IRuleBuilder<T, string> ruleBuilder)
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

    }
}
