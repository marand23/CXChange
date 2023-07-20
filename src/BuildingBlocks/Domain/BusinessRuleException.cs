using CXChange.BuildingBlocks.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain
{
    public class BusinessRuleException:Exception
    {
        public IBussinessRuleSpecification _specification;

        public string Message { get; }

        public BusinessRuleException(IBussinessRuleSpecification specification)
            
        {
           this.Message = specification.Message;
        }

        //public override string ToString()
        //{
        //    return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        //}
    }
}
