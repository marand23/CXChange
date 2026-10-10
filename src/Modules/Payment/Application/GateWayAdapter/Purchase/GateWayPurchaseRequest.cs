namespace CXChange.Modules.Payment.Application.GateWayAdapter.Purchase
{
    public record GateWayPurchaseRequest
    {
        public string PhoneNumber { get; init; }
        public decimal Amount { get; init; }
        public string DepositId { get; init; }
        public string Currency { get; init; }
        public string CardNumber { get; init; }
        public string Desciption { get; init; }

    }
}
