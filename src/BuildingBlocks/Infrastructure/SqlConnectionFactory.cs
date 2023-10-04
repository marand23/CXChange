using CXChange.BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CXChange.CoreElements.Infrastructure
{
    internal class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private string _connectionString;
        private IDbConnection _connection;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public IDbConnection CreateNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = new SqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }
        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }


    }
}
