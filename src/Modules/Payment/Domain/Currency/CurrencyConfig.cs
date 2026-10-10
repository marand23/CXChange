using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Currency.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Currency
{
    public class CurrencyConfig : Entity<int>, IAggregateRoot
    {
        private CurrencyConfig() { }
        public Currency.ValueObjects.Currency Currency { get; private set; }
        public DepositWithdrawalStatus DepositWithdrawStatus { get; private set; }
        //public List<CurrencyNetwork> Networks { get; private set; }
        //public void AddNetwork(string name, string network, decimal withdrawFee, bool depositEnable, bool withdrawEnable, int minConfirm, string addressRegex, string memoRegex, decimal withdrawMin, decimal withdrawMax)
        //{
        //    Networks = new List<CurrencyNetwork>();
        //    var currencyNetwork = new CurrencyNetwork
        //    {
        //        NetworkDepositWithdrawStatus = new NetworkDepositWithdrawStatus(depositEnable, withdrawEnable),
        //        NetworkMinConfirm = new NetworkMinConfirm { MinConfirm = minConfirm },
        //        NetworkName = new NetworkName { Name = name, Network = network },
        //        NetworkRegex = new NetworkRegex(addressRegex, memoRegex),
        //        NetworkWithdrawLimits = new NetworkWithdrawLimits(withdrawMin, withdrawMax),
        //    };
        //    Networks.Add(currencyNetwork);
        //}
    }

}
