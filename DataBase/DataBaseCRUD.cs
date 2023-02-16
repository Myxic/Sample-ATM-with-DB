using DataBase;
using DataBase.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using static DataBase.Model.User;

namespace DataBase
{
    public class DataBaseCRUD : IAtmDBServies
    {
        private readonly AtmDBContext _dbContext;
        private bool _disposed;

        public DataBaseCRUD(AtmDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> CreateUser(User user)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();


            string insertQuery =
                $@"INSERT INTO ATMDB (First_name, Last_name, UserName, Gender, Card_No, Balance, Pin_No, Phone_Number)
                 VALUES (@First_name, @Last_name, @UserName, @Gender, @Card_No, @Balance, @Pin_No, @Phone_Number); SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";


            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@First_name",
                    Value = user.First_name,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Last_name",
                    Value = user.Last_name,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = user.UserName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = user.Gender,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Card_No",
                    Value = user.Card_No,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Balance",
                    Value = user.Balance,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    //Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Pin_No",
                    Value = user.Pin_No,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    //Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Phone_Number",
                    Value = user.Phone_Number,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }

            });

            // await command.ExecuteNonQueryAsync();


            // SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            long userId = (long)await command.ExecuteScalarAsync();

            return userId;


        }

        public async Task<bool> UpdateUser(string UserName, User user)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();



            string insertQuery =
                $"UPDATE ATMDB SET First_name = @First_name, Last_name = @Last_name, UserName = @UserName, Gender = @Gender, Card_No =  @Card_No, Balance = @Balance, Pin_No = @Pin_No, Phone_Number = @Phone_Number WHERE UserName = @UserName ";

            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@First_name",
                    Value = user.First_name,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Last_name",
                    Value = user.Last_name,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },

                new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = user.UserName,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = user.Gender,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Card_No",
                    Value = user.Card_No,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Balance",
                    Value = user.Balance,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    //Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Pin_No",
                    Value = user.Pin_No,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    //Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@Phone_Number",
                    Value = user.Phone_Number,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = UserName,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }

            });

            var result = command.ExecuteNonQuery();

            return (result == 0) ? false : true;

        }

        public async Task<bool> DeleteUser(int UserId)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string deleteQuery = $"DELETE FROM ATMDB WHERE id = @id ";
            await using SqlCommand command = new SqlCommand(deleteQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@id",
                    Value = UserId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
            });

            var result = command.ExecuteNonQuery();


            return (result != 0);


        }

        public async Task<User> GetUser(string UserName)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getUserQuery = $"SELECT ATMDB.First_name, ATMDB.Last_name, ATMDB.UserName, ATMDB.Gender, ATMDB.Card_No, ATMDB.Balance, ATMDB.Pin_No, ATMDB.Phone_Number FROM  ATMDB WHERE UserName = @UserName ";
            await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = UserName,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
            });
            User user = new User();
            using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {
                while (dataReader.Read())
                {
                    user.First_name = dataReader["First_name"].ToString();
                    user.Last_name = dataReader["Last_name"].ToString();
                    user.UserName = dataReader["UserName"].ToString();
                    user.Gender = dataReader["Card_No"].ToString();
                    user.Balance = (int)dataReader["Balance"];
                    user.Pin_No = (int)dataReader["Pin_No"];
                    user.Phone_Number = dataReader["Phone_Number"].ToString();
                }
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();
            string getAllUsersQuery = $"SELECT ATMDB.First_name, ATMDB.Last_name, ATMDB.UserName, ATMDB.Gender, ATMDB.Card_No, ATMDB.Balance, ATMDB.Pin_No, ATMDB.Phone_Number FROM  ATMDB";
            await using SqlCommand command = new SqlCommand(getAllUsersQuery, sqlConn);
            List<User> users = new List<User>();
            using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {
                while (dataReader.Read())
                {
                    users.Add(
                        new User()
                        {
                            First_name = dataReader["First_name"].ToString(),
                            Last_name = dataReader["Last_name"].ToString(),
                            UserName = dataReader["UserName"].ToString(),
                            Gender = dataReader["Card_No"].ToString(),
                            Balance = (int)dataReader["Balance"],
                            Pin_No = (int)dataReader["Pin_No"],
                            Phone_Number = dataReader["Phone_Number"].ToString()
                            
                        }
                        );
                }

            }

            return users;
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