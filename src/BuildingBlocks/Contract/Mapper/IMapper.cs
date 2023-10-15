namespace CXChange.BuildingBlocks.Contract.Mapper
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
