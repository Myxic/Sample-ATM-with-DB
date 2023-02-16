using System;
using System.Data;
using System.Threading.Tasks;
using DataBase.Model;
using Microsoft.Data.SqlClient;

namespace DataBase
{
    public class AtmDBContext : IDisposable
    {

        private readonly string _connString;

        private bool _disposed;

        private SqlConnection _dbConnection = null;

        //public AtmDBContext() : this(@"Data Source = localhost,1433;User Id=sa; Password=Strong.Pwd-123; Initial Catalog=ATMDATABASE; Encrypt=False;Integrated Security=True; TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        //{
            
        //}
        public AtmDBContext() : this(@"data source = dd0ce83250fd; initial catalog = ATMDATABASE; user id = sa; password = &lt; &lt; YourPassword & gt; &gt;")
        { 
	    }
        public AtmDBContext(string connString)
        {
            _connString = connString;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public async Task CloseConnection()
        {
            if (_dbConnection?.State != ConnectionState.Closed)
            {
                await _dbConnection?.CloseAsync();
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

