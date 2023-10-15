using CXChange.BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

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
            //var connection = new MySqlConnection(_connectionString);
            var connection = _connection;
            connection.Open();

            return connection;
        }

        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                // this._connection = new SqlConnection(_connectionString);
                this._connection = new MySqlConnection(_connectionString);
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
