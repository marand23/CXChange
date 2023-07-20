using MediatR.NotificationPublishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.BuildingBlocks.Domain.Specification
{
    public class OrSpecification:ISpecification
    {
        private readonly ISpecification _spec1;
        private readonly ISpecification _spec2;

        public bool isSatisfiedBy()
        {
           return _spec1.isSatisfiedBy() || _spec2.isSatisfiedBy();
        }
    }
}
