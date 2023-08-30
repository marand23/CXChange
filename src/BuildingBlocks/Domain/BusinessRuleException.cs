using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.BuildingBlocks.Domain
{
    public class BusinessRuleException : Exception
    {


        public string Message { get; }

        public Enum Code { get; }
        public BusinessRuleException(IBussinessRuleSpecification specification)

        {
            this.Message = specification.Message;
            this.Code = specification.Code;

        }

        //public override string ToString()
        //{
        //    return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        //}
    }
}
