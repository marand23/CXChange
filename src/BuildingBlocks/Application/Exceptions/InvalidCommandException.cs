namespace CXChange.BuildingBlocks.Application.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public string[] Errors { get; }
        public InvalidCommandException(string[] erros) { Errors = erros; }
    }
}
