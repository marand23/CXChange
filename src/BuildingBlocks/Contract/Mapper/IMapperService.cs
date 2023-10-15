namespace CXChange.BuildingBlocks.Contract.Mapper
{
    public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource source);

    }
}
