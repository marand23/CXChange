using CXChange.BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.CoreElements.Infrastructure
{
    internal class SqlConnectionFactory:ISqlConnectionFactory,IDisposable
    {
        private readonly IConfiguration configuration;
        private IDbConnection _connection;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        

        public IDbConnection CreateNewConnection()
        {
            throw new NotImplementedException();
        }

        public IDbConnection GetOpenConnection()
        {
            throw new NotImplementedException();
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
