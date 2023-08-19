namespace CXChange.API
{

    public class ApiErrorResponse : IApiError
    {
        public string message { get; set; }
        public int code { get; set; }
        public bool success { get; set; }
        public static implicit operator ApiErrorResponse(ApiErrorModel error)
        {
            return new ApiErrorResponse { code = error.code, message = error.message, success = false };


        }
    }
    public class ApiSuccessResponse<T>
    {
        public string message { get; set; }
        public bool success { get; set; }
        public T data { get; set; }
    }
    public class ApiValidationErrorModel : IApiError
    {
        public string message { get; set; }
        public bool success { get; set; }
        public List<ValidationError> errors { get; set; }


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
    public interface IError
    {
        ApiErrorModel GetError(string innerMessage);

    }
    public class ApiErrorModel
    {
        public string message { get; set; }
        public int code { get; set; }
    }

}

