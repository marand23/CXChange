using CXChange.API.ResourceFiles;
using CXChange.BuildingBlocks.Application.Error;
using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Application.Exceptions;

namespace CXChange.API
{

    public class ApiErrorResponse : IApiError
    {
        public string message { get; init; }
        public int code { get; init; }
        public bool success { get; init; }
        public object data { get; init; }
        public static implicit operator ApiErrorResponse(Exception ex)
        {
            if (ex is FailedAttempException fa)
            {
                return new ApiErrorResponse { code = Convert.ToInt32(fa.ErrorCode), message = string.Format(CommandErrorMessages.ResourceManager.GetString(fa.ErrorCode.ToString()), fa.Detail.Remainig), success = false, data = fa.Detail };
            }
            //else if (ex is GatewayException gw)
            //{

            //    return new ApiErrorResponse { code = gw.ErrorCode, message = GatewayErrorMessages.ResourceManager.GetString(gw.ErrorCode.ToString()), success = false, data = gw.TrackId };
            //}
            else if (ex is ExchangeCommandException e)
            {



                return new ApiErrorResponse { code = Convert.ToInt32(e.ErrorCode), message = CommandErrorMessages.ResourceManager.GetString(e.ErrorCode.ToString()), success = false };
            }
            else if (ex is BusinessRuleException be)
            {


                return new ApiErrorResponse { code = Convert.ToInt32(be.Code), message = DomainErrorMessages.ResourceManager.GetString(be.Code.ToString()), success = false };
            }

            else
            {
                return new ApiErrorResponse { code = Convert.ToInt32(status.ServerError), message = "Unknown Error", success = false };

            }
        }
    }
    public record ApiSuccessResponse<T>
    {
        public string message { get; init; }
        public bool success { get; init; }
        public T data { get; init; }

    }
    public record ApiValidationResponse : IApiError
    {
        public string message { get; init; }
        public bool success { get; init; }
        public List<ValidationError1> errors { get; init; }
        public static implicit operator ApiValidationResponse(ValidationError error)
        {
            var error1 = error.Errors.Select(x => new ValidationError1
            {
                Code = (ValidationErrorCodes)Enum.Parse(typeof(ValidationErrorCodes), x.ErrorCode),
                Name = x.PropertyName,

                Message = ValidationErrorMessages.ResourceManager.GetString(x.ErrorCode.ToString()),
            }).ToList(); ;
            return new ApiValidationResponse { errors = error1, message = "Validation Failed", success = false };
        }


    }
    public class ValidationError1
    {
        public string Name { get; init; }
        public string Message { get; init; }
        public ValidationErrorCodes Code { get; init; }

    }

    public interface IApiError
    {

    }
    public enum ValidationErrorCodes
    {
        //error codes should be of format :-1xx
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
        NationalIdValidator,
        AgeValidator,
        BirthDayValidator

    }
    public enum status
    {
        ServerError = 500
    }




}

