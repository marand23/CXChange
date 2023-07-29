using CXChange.BuildingBlocks.Application.MessageInterfaces;
using CXChange.BuildingBlocks.Domain;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.CommandFilters
{
    public class UnitOfWorkFilter<T> : IFilter<ConsumeContext<T>> where T : class, IPersistable
    {
        private IUnitOfWork _unitOfWork;
        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Probe(ProbeContext context)
        {

        }





        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);
            await _unitOfWork.CommitChangesAsync();
        }
    }
}
