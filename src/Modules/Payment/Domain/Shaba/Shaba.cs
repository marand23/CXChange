using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.BankCard.ValueObjects;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using CXChange.Modules.Payment.Domain.ValueObjects;

namespace CXChange.Modules.Payment.Domain.Shaba
{
    public class Shaba : Entity<Guid>, IAggregateRoot
    {
        private Shaba() { }
        private Shaba(string shabaNumber, Guid userid, Guid id)
        {
            ShabaNumber = new WithdrawAddress(shabaNumber);
            BankName = new Bank("");
            UserId = userid;
            Id = id;
        }

        public Guid UserId { get; private set; }
        public WithdrawAddress ShabaNumber { get; private set; }
        public Bank BankName { get; private set; }
        public ShabaStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public static Shaba Create(string shabaNumber, Guid userId, Guid id)
        {
            return new Shaba(shabaNumber, userId, id);
        }
        public void ConfirmShaba()
        {

        }
        public void RejectShaba()
        {

        }
        public void SetForAdminApproval()
        {

        }


    }
}