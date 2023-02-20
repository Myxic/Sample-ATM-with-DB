using System;
using System.Threading.Tasks;
using DataBase;
using DataBase.Model;
using Microsoft.Data.SqlClient;

namespace ATMOperations
{
    public class TransferOperation
    {
        private readonly AtmDBContext _dbContext;
        private bool _disposed;

        public TransferOperation(AtmDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected void Transfer(string language)
        {

            switch (language)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                default:
                    break;
            }
        }
        public decimal GetUserBalance(User user)
        {

            decimal Balance = Convert.ToDecimal(user.Balance);
            return Balance;
        }

        public async Task<User> GetUserDetails(string UserName)
        {
            try
            {
                SqlConnection sqlConn = await _dbContext.OpenConnection();
                string getUserQuery = @$"SELECT ATMDB.First_name, ATMDB.Last_name, ATMDB.UserName, ATMDB.Gender, ATMDB.Card_No, ATMDB.Balance, ATMDB.Pin_No, ATMDB.Phone_Number FROM  ATMDB WHERE UserName = '{UserName}' "; ;

                await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
                User reveiver = new User();
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        reveiver.First_name = dataReader["First_name"].ToString();
                        reveiver.Last_name = dataReader["Last_name"].ToString();
                        reveiver.UserName = dataReader["UserName"].ToString();
                        reveiver.Gender = dataReader["Card_No"].ToString();
                        reveiver.Balance = dataReader["Balance"].ToString();
                        reveiver.Pin_No = dataReader["Pin_No"].ToString();
                        reveiver.Phone_Number = dataReader["Phone_Number"].ToString();
                    }
                }

                return reveiver;
            }
            catch (Exception ex)
            {
                //Console.Clear();
                Console.WriteLine($"{UserName} is invalid or {ex.Message}");
                return null;
            }
        }

        public decimal Transation(decimal CashtoTransfer, User user)
        {

            if (CashtoTransfer > 0 && CashtoTransfer! < Convert.ToDecimal(user.Balance))
            {
                decimal RemainingCash = (Convert.ToDecimal(user.Balance) - CashtoTransfer);
                return RemainingCash;
            }
            else
            {
                return CashtoTransfer;
            }

        }

        public async Task<bool> UpdateDB(User user, decimal RemainingCash)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string insertQuery = $"UPDATE ATMDB SET Balance = {(int)RemainingCash} WHERE UserName = '{user.UserName}'";

            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            var result = command.ExecuteNonQuery();

            return (result == 0) ? false : true;
        }

        public async Task<bool> UpdateReceiver(User receiver, decimal CashTransfer)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string insertQuery =
                $"UPDATE ATMDB SET Balance = {(int)((Convert.ToDecimal(receiver.Balance)) + CashTransfer)} WHERE UserName = '{receiver.UserName}'";

            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            var result = command.ExecuteNonQuery();

            return (result == 0) ? false : true;
        }

        public async Task<bool> UpdatePinCode(User user, string Pin)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string insertQuery =
                $"UPDATE ATMDB SET Pin_No = {(int)(Convert.ToDecimal(Pin))} WHERE UserName = '{user.UserName}'";

            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            var result = command.ExecuteNonQuery();

            return (result == 0) ? false : true;
        }

    }
}

