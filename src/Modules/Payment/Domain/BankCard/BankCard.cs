using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.BankCard.ValueObjects;
using CXChange.Modules.Payment.Domain.ValueObjects;

namespace CXChange.Modules.Payment.Domain.BankCard
{
    public class BankCard : Entity<Guid>, IAggregateRoot
    {
        private BankCard() { }
        private BankCard(string cardNumber, string bankName, Guid userId, Guid id)
        {
            CardNumber = new DepositAddress(cardNumber);
            BankName = new Bank(bankName);
            UserId = userId;
            Id = id;
        }
        public Guid UserId { get; private set; }
        public DepositAddress CardNumber { get; private set; }
        public Bank BankName { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public BankCardStatus Status { get; private set; }
        public static BankCard Create(string cardNumber, string bankName, Guid userId, Guid id)
        {
            return new BankCard(cardNumber, bankName, userId, id);
        }
        public void VerifyBankCard(string bankName)
        {
            BankName = new Bank(bankName);
            Status = BankCardStatus.Verified;
        }
        public void RejectBankCard()
        {
            Status = BankCardStatus.Rejected;
        }
        public void SetForAdminApproval()
        {
            Status = BankCardStatus.WaitingAdminVerification;
        }
    }
}
