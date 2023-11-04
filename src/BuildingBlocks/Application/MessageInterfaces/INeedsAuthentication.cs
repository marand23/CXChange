namespace CXChange.BuildingBlocks.Application.MessageInterfaces
{
    public interface INeedsAuthentication
    {
        Guid UserId { get; set; }
    }
}
