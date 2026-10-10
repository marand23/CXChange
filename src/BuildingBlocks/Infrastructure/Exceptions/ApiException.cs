namespace CXChange.BuildingBlocks.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {

        public ApiException(string message) : base(message)
        {
        }
        public override string ToString()
        {
            return this.Message;
        }
    }
}
