namespace CXChange.Modules.Kyc.Domain.User.Enums
{
    public enum KycStatus
    {
        Unverified,
        InProgress,//under review
        Verified,
        Rejected,//rejected but it can be repeated
        Denied //user can not do kyc once again
    }
}
