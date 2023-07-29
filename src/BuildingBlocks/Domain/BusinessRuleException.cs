using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.BuildingBlocks.Domain
{
    public class BusinessRuleException : Exception
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
