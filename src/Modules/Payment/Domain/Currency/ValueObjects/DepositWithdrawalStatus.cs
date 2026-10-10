namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record DepositWithdrawalStatus
    {
        private DepositWithdrawalStatus() { }
        public DepositWithdrawalStatus(bool depositEnable, bool withdrawEanble)
        {
            DepositEnable = depositEnable;
            WithdrawEnable = withdrawEanble;
        }
        public bool DepositEnable { get; init; }
        public bool WithdrawEnable { get; init; }
    }
}
