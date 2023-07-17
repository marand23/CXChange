using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(List<IDomainEvent> domainEvents);
    }
}
