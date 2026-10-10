using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CXChange.BuildingBlocks.Infrastructure.Data
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
            //var connection = new MySqlConnection(_connectionString);
            var connection = _connection;
            connection.Open();

            return connection;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                // this._connection = new SqlConnection(_connectionString);
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }


    }
}
