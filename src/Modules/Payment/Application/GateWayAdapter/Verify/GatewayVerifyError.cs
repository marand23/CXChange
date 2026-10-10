namespace CXChange.Modules.Payment.Application.GateWayAdapter.Verify
{
    public enum GatewayVerifyError
    {

        FailedOrNotPaidYet,
        AlreadyVerified,
        Unknown,
        Reversed,
        Failed,
        NotVerifiable

    }
}
