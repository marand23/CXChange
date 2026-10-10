using FluentValidation.Results;

namespace CXChange.BuildingBlocks.Application.Exceptions
{
    public class ValidationException : Exception
    {

        public List<ValidationFailure> Errors { get; }
        public ValidationException(List<ValidationFailure> erros)
        {
            Errors = erros;
        }
    }

}
