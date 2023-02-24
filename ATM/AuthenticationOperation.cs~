using System;
using DataBase.Model;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using DataBase;
using System.Data;

namespace ATMSystem
{
    public class AuthenticationOperation
    {

        private readonly AtmDBContext _dbContext;
        private bool _disposed;

        public AuthenticationOperation(AtmDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> CheckUser(string Card_No)
        {

            try
            {
                SqlConnection sqlConn = await _dbContext.OpenConnection();
                string VerCardNumber;
                string getUserQuery = @$"SELECT *  FROM ATMDB  WHERE ATMDB.Card_No = '{Card_No}' ";

                await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);


                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        VerCardNumber = dataReader["Card_No"].ToString();
                        switch (VerCardNumber)
                        {
                            case null:
                                return false;
                            case " ":
                                return false;
                            default:
                                return true;
                        }
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<bool> CheckPin(string Pin, string Card_No)
        {
            try
            {
                string VerCardPin;
                SqlConnection sqlConn = await _dbContext.OpenConnection();
                string getUserQuery = $"SELECT ATMDB.Pin_No from ATMDB WHERE Card_No = '{Card_No}' ";
                await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        VerCardPin = dataReader["Pin_No"].ToString();
                        return (VerCardPin == Pin) ? true : false;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<User> GetUserDetails(string Card_No)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"SELECT ATMDB.id, ATMDB.First_name, ATMDB.Last_name, ATMDB.UserName, ATMDB.Gender, ATMDB.Card_No, ATMDB.Balance, ATMDB.Pin_No, ATMDB.Phone_Number FROM  ATMDB WHERE Card_No = '{Card_No}' ";
            await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);

            User user = new User();
            using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {
                while (dataReader.Read())
                {

                    user.First_name = dataReader["First_name"].ToString();
                    user.Last_name = dataReader["Last_name"].ToString();
                    user.UserName = dataReader["UserName"].ToString();
                    user.Gender = dataReader["Card_No"].ToString();
                    user.Card_No = dataReader["Card_No"].ToString();
                    user.Balance = dataReader["Balance"].ToString();
                    user.Pin_No = dataReader["Pin_No"].ToString();
                    user.Phone_Number = dataReader["Phone_Number"].ToString();
                }
            }

            return user;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
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

