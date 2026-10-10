namespace CXChange.Modules.Payment.Domain.Interfaces
{
    public interface IBankCardRepository
    {
        Task<BankCard.BankCard> GetBankCardByCardNumberAsync(string cardNumber);
        Task<List<BankCard.BankCard>> GetUserBankCardsAsync(Guid userId);
        Task<bool> IsRepeatedBankCardAsync(string cardNubmer);
        Task AddBankCardAsync(BankCard.BankCard bankCard);
        Task<BankCard.BankCard> GetUserBankCardByCardNumber(string cardNumber, Guid userId);


    }
}
