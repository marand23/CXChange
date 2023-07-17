using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> CommitChangesAsync(CancellationToken cancellationToken=default);
    }
}
