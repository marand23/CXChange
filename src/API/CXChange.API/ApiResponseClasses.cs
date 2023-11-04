using CXChange.API.ResourceFiles;
using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Exceptions;

namespace CXChange.API
{

    public class ApiErrorResponse : IApiError
    {
        public string message { get; set; }
        public Enum code { get; set; }
        public bool success { get; set; }
        public object data { get; set; }
        public static implicit operator ApiErrorResponse(Exception ex)
        {
            if (ex is FailedAttempException fa)
            {
                return new ApiErrorResponse { code = fa.ErrorCode, message = string.Format(CommandErrorMessages.ResourceManager.GetString(fa.ErrorCode.ToString()), fa.Detail.Remainig), success = false, data = fa.Detail };
            }
            else if (ex is ExchangeCommandException e)
            {



                return new ApiErrorResponse { code = e.ErrorCode, message = CommandErrorMessages.ResourceManager.GetString(e.ErrorCode.ToString()), success = false };
            }
            else if (ex is BusinessRuleException be)
            {
                return new ApiErrorResponse { code = be.Code, message = DomainErrorMessages.ResourceManager.GetString(be.Code.ToString()), success = false };
            }
            else
            {
                return new ApiErrorResponse { code = status.ServerError, message = "Unknown Error", success = false };

            }
        }
    }
    public class ApiSuccessResponse<T>
    {
        public string message { get; init; }
        public bool success { get; init; }
        public T data { get; init; }

    }
    public class ApiValidationResponse : IApiError
    {
        public string message { get; set; }
        public bool success { get; set; }
        public List<ValidationError> errors { get; set; }
        public static implicit operator ApiValidationResponse(ValidationException ex)
        {
            List<ValidationError> error = ex.Errors
            .Select(e => new ValidationError { Code = (ValidationErrorCodes)Enum.Parse(typeof(ValidationErrorCodes), e.ErrorCode), Message = ValidationErrorMessages.ResourceManager.GetString(e.ErrorCode), Name = e.PropertyName }).ToList();
            return new ApiValidationResponse { errors = error, message = "Validation Failed", success = false };
        }


    }
    public class ValidationError
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public ValidationErrorCodes Code { get; set; }

    }

    public interface IApiError
    {

    }
    public enum ValidationErrorCodes
    {
        //erro codes should be of format :-1xx
        NotEmptyValidator = -199,
        ExactLengthValidator,
        RegularExpressionValidator,
        EmailValidator,
        GreaterThanOrEqualValidator,
        GreaterThanValidator,
        LengthValidator,
        MinimumLengthValidator,
        MaximumLengthValidator,
        LessThanOrEqualValidator,
        LessThanValidator,
        NotEqualValidator,
        NotNullValidator,
        PredicateValidator,
        AsyncPredicateValidator,
        EqualValidator,
        InclusiveBetweenValidator,
        ExclusiveBetweenValidator,
        CreditCardValidator,
        ScalePrecisionValidator,
        EmptyValidator,
        NullValidator,
        EnumValidator,
        Length_Simple,
        MinimumLength_Simple,
        MaximumLength_Simple,
        ExactLength_Simple,
        InclusiveBetween_Simple,
        PasswordValidator,
        PhoneNumberValidator,
        VerificationCodeValidator,
        NotEmptyNotNullValidator,
        PageSizeValidator,
        PageValidator,
        OrderByValidator,
        KycImageExtensionValidator,
        KycImageSizeValidator,
        FullNameValidator,
        NationalIdValidator

    }
    public enum status
    {
        ServerError = 500
    }




}

