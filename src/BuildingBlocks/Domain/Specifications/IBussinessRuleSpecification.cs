using CXChange.BuildingBlocks.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain.Specifications
{
    public interface IBussinessRuleSpecification:ISpecification
    {

         string Message { get; }
    }
}
