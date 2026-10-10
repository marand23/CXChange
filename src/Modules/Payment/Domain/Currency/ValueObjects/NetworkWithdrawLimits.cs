namespace CXChange.Modules.Payment.Domain.Currency.ValueObjects
{
    public record NetworkWithdrawLimits
    {
        public NetworkWithdrawLimits(decimal withdrawMin, decimal withdrawMax)
        {
            if (withdrawMin < withdrawMax)
            {
                throw new Exception();
            }
            WithdrawMax = withdrawMax;
            WithdrawMin = withdrawMin;

        }
        public decimal WithdrawMin { get; init; }
        public decimal WithdrawMax { get; init; }

    }
}
