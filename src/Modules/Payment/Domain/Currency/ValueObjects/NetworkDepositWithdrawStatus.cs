namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record NetworkDepositWithdrawStatus
    {
        private NetworkDepositWithdrawStatus() { }
        public NetworkDepositWithdrawStatus(bool depositEnable, bool withdrawEnable)
        {
            DepositEnable = depositEnable;
            WithdrawEnable = withdrawEnable;
        }
        public bool DepositEnable { get; init; }
        public bool WithdrawEnable { get; init; }

    }
}
