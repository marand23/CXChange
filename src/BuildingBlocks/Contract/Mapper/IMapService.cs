namespace CXChange.BuildingBlocks.Contract.Mapper
{
    public interface IMapService
    {
        TDestination Map<TSource, TDestination>(TSource source, object extraData = null);

    }
}
