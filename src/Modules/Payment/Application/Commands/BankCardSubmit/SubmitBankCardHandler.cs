using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.Shaba;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.BankCardAdd
{
    internal class SubmitBankCardHandler : IConsumer<SubmitBankCard>
    {
        public IUserRepository _repo;
        public IUnitOfWork _uow;
        public IBankCardRepository _bankCardRepository;
        public IShabaRepository _shabaRepository;
        public SubmitBankCardHandler(IUserRepository userRepository, [FromKeyedServices("payment")] IUnitOfWork uow, IBankCardRepository bankCardRepository, IShabaRepository shabaRepository)
        {
            _repo = userRepository;
            _uow = uow;
            _bankCardRepository = bankCardRepository;
            _shabaRepository = shabaRepository;
        }
        public async Task Consume(ConsumeContext<SubmitBankCard> context)
        {
            // Console.WriteLine(new MonetaryValue(123.1234M, new Currency("BTC", 0.0001M)).ToString());

            var user = await _repo.FindById(context.Message.UserId);
            var bankCard = Shaba.Create(context.Message.CardNumber, user.Id, NewId.NextSequentialGuid());
            await _shabaRepository.AddShabaAsync(bankCard);

            await _uow.CommitChangesAsync();
            await context.RespondAsync(new BankCardSubmitted { });
        }
    }

}
