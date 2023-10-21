using System.Data;

namespace CXChange.BuildingBlocks.Infrastructure.Data
{
    internal interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
        IDbConnection CreateNewConnection();
    }
}
