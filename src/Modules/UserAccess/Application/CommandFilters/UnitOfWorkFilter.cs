using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.CommandFilters
{
    public class UnitOfWorkFilter<T> : IFilter<ConsumeContext<T>> where T : class
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
