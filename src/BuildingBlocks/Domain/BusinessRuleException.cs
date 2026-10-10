using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.BuildingBlocks.Domain
{
    public class BusinessRuleException : Exception
    {

        public IBussinessRuleSpecification _specification { get; }


        public Enum Code { get; }
        public BusinessRuleException(IBussinessRuleSpecification specification) : base(specification.Message)

        {

            this.Code = specification.Code;
            _specification = specification;

        }
        protected BusinessRuleException(Enum code, string message) : base(message)
        {
            Code = code;

        }


    }
}
