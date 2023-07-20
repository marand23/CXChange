using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain.Specification
{
    public interface ISpecification
    {
        bool isSatisfiedBy();

    }
}
