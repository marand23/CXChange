namespace CXChange.Modules.Payment.Application.GateWayAdapter.Purchase
{
    public enum GatewayPurchaseError
    {
        CanceledByUser,
        TransactionTimedOut,
        CardDoesNotMatch,
        Unkown,
        InternalError,
        NotEnoughBalance,
        PinNotValid,
        RequestLimit,
        InvalidCardIssuer,
        SwitchIssue,
        CardNotAccessable,
        GatewayCreateLinkFailed,// when we want to get the paylink in first step from gateway in that case if gate way does not respond or if the reposonse if not the expected response we will use thi ,others in this enum are for the errors after payment(callback or inquriy)
        Other




    }
}
