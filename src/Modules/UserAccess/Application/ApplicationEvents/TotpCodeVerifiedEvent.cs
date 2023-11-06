using CXChange.Modules.UserAccess.Application.Utils;

namespace CXChange.Modules.UserAccess.Application.ApplicationEvents
{
    internal record TotpCodeVerifiedEvent
    {
        public Guid UserId { get; set; }
        public CodeType CodeType { get; set; }
        public string Info { get; set; }
    }
}
