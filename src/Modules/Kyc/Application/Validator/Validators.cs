using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.Application.Validator
{
    public static class Validators
    {

        public static IRuleBuilderOptions<T, IFormFile> KycPhoto<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            return ruleBuilder.NotNull().NotEmpty().Must(x => CheckImageExtensionAndSize.CheckExtension(x)).WithErrorCode("KycImageExtensionValidator")
                .Must(x => CheckImageExtensionAndSize.CheckKycImageSignature(x)).WithErrorCode("KycImageExtensionValidator").Must(x => CheckImageExtensionAndSize.CheckKycImageSize(x, 1000000)).WithErrorCode("KycImageSizeValidator");

        }
        public static IRuleBuilderOptions<T, string> NationalId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().NotNull().Length(10).Must(x => CheckNationalId.CheckNationalIdAuthenticity(x)).WithErrorCode("NationalIdValidator");
        }
        public static IRuleBuilderOptions<T, string> FullName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().NotNull().Matches(" \"^[آ-ی ء چ]+$\"").WithErrorCode("FullNameValidator");
        }

    }
}

