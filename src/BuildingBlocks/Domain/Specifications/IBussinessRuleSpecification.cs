using CXChange.BuildingBlocks.Domain.Specification;

namespace CXChange.BuildingBlocks.Domain.Specifications
{
    public interface IBussinessRuleSpecification : ISpecification
    {

        string Message { get; }
    }
}
