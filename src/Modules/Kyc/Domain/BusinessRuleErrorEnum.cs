namespace CXChange.Modules.Kyc.Domain
{
    internal enum BusinessRuleErrorEnum
    {
        //enum range should be be of format: 5XXX
        KycAlreadyVerified = -5999,
        KycVerificationInProgress,
        KycVerificationIsNotPossible,
        NationalIdIsNotUnique,
        KycIsNotInProgressOnApproval,
        KycAlreadyApprovedOnRejection

    }
}
