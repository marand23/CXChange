using CXChange.Modules.UserAccess.Application.Enums;

namespace CXChange.Modules.UserAccess.Application.Exceptions
{
    public class FailedAttempException : UserAccessCommandException
    {
        public FailedAttempDetail Detail { get; }
        public FailedAttempException(CommandExceptionEnum errorCodes, string message = "failed attemp in useraccess", FailedAttempDetail detail = null) : base(errorCodes, message)
        {
            Detail = detail;
        }


    }
    public class FailedAttempDetail
    {
        public int Remainig { get; set; }
        public DateTime RetryAfter { get; set; }
    }

}
