using FluentValidation.Results;

namespace CXChange.BuildingBlocks.Application.Error
{
    public class ValidationError : IError
    {
        public List<ValidationFailure> Errors { get; }
        public ValidationError(List<ValidationFailure> erros)
        {
            Errors = erros;
        }
    }
}
