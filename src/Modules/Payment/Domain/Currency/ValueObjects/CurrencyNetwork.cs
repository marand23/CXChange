namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record CurrencyNetwork
    {


        public NetworkName NetworkName { get; init; }
        public NetworkRegex NetworkRegex { get; init; }
        public NetworkWithdrawLimits NetworkWithdrawLimits { get; init; }
        public NetworkMinConfirm NetworkMinConfirm { get; init; }
        public NetworkDepositWithdrawStatus NetworkDepositWithdrawStatus { get; init; }

    }
}
