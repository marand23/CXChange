using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.BuildingBlocks.Domain;

namespace CXChange.API
{

    public class ApiErrorResponse : IApiError
    {
        public string message { get; set; }
        public Enum code { get; set; }
        public bool success { get; set; }
        public static implicit operator ApiErrorResponse(Exception ex)
        {

            if (ex is ExchangeCommandException e)
            {
                return new ApiErrorResponse { code = e.ErrorCode, message = e.Message, success = false };
            }
            else if (ex is BusinessRuleException be)
            {
                return new ApiErrorResponse { code = be.Code, message = be.Message, success = false };
            }
            else
            {
                return new ApiErrorResponse { code = status.ServerError, message = "Unknown Error", success = false };

            }
        }
    }
    public class ApiSuccessResponse<T>
    {
        public string message { get; set; }
        public bool success { get; set; }
        public T data { get; set; }

    }
    public class ApiValidationResponse : IApiError
    {
        public string message { get; set; }
        public bool success { get; set; }
        public List<ValidationError> errors { get; set; }
        public static implicit operator ApiValidationResponse(ValidationException ex)
        {
            ValidationErrorCodes errorCodes;
            List<ValidationError> error = ex.Errors
            .Select(e => new ValidationError { Code = (ValidationErrorCodes)Enum.Parse(typeof(ValidationErrorCodes), e.ErrorCode), Message = e.ErrorMessage, Name = e.PropertyName }).ToList();
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
        InclusiveBetween_Simple

    }
    public enum status
    {
        ServerError
    }




}

