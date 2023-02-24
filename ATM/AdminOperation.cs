using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DataBase;
using DataBase.Model;
using Microsoft.Data.SqlClient;
using static ATMSystem.InputFormat;
namespace ATMSystem
{
    public class AdminOperation
    {
        private readonly AtmDBContext _dbContext;
        private bool _disposed;
        private static int count;
        DataBaseCRUD data = new DataBaseCRUD(new AtmDBContext());
        AuthenticationOperation authentication = new AuthenticationOperation(new AtmDBContext());
        //ATMUsersOperations atmOperations = new ATMUsersOperations(new AtmDBContext());


        public AdminOperation(AtmDBContext dbContext)
        {
            _dbContext = dbContext;
            count = 0;
        }


        public async Task<Admin> GetAdminDetails(string PinCode)
        {
            try
            {
                SqlConnection sqlConn = await _dbContext.OpenConnection();
                string getUserQuery = @$"SELECT BankAdmin.First_name, BankAdmin.Last_name, BankAdmin.Gender, BankAdmin.PinCode, BankAdmin.Phone_Number FROM  BankAdmin WHERE PinCode = '{Convert.ToInt32(PinCode)}' "; 

                await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);
                Admin admin = new Admin();
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        admin.First_name = dataReader["First_name"].ToString();
                        admin.Last_name = dataReader["Last_name"].ToString();
                        admin.Gender = dataReader["Gender"].ToString();
                        admin.Pin_No = dataReader["PinCode"].ToString();
                        admin.Phone_Number = dataReader["Phone_Number"].ToString();
                    }
                }

                return admin;
            }
            catch (Exception ex)
            {
                //Console.Clear();
                Console.WriteLine($"{PinCode} is invalid or {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AdminVerify(string Pin)
        {

            try
            {
                SqlConnection sqlConn = await _dbContext.OpenConnection();
                string VerCardNumber;
                string getUserQuery = @$"SELECT *  FROM BankAdmin  WHERE BankAdmin.PinCode = '{Convert.ToInt32(Pin)}' ";

                await using SqlCommand command = new SqlCommand(getUserQuery, sqlConn);


                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        VerCardNumber = dataReader["PinCode"].ToString();
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

        public async Task CreateAccountAsync()
        {
            string FirstName = InputFormat.FirstName();
            Console.Clear();
            string Lastname = InputFormat.LastName();
            Console.Clear();
            string Username = InputFormat.UserName();
            Console.Clear();
            string CardNo = InputFormat.CardNo();
            Console.Clear();
            string Balance = InputFormat.Balance();
            Console.Clear();
            string Pin_No = InputFormat.PinNo();
            Console.Clear();
            string Gender = InputFormat.Gender();
            Console.Clear();
            string PhoneNo = InputFormat.PhoneNumber();
            Console.Clear();
            try
            {

               here: Console.Write("VERIFY WHETHER YOUR AN ADMIN TO CONTINUE \n\tEnter your PinCode \n\t ===>");
                string PinCode = Console.ReadLine();

                switch (await AdminVerify(PinCode))
                {
                    case true:

                   using (IAtmDBServies ATMService = new DataBaseCRUD(new AtmDBContext()))
                    {
                        var userData = new User
                        {
                            First_name = FirstName,
                            Last_name = Lastname,
                            Gender = Gender,
                            Pin_No = Pin_No,
                            Balance = Balance,
                            UserName = Username,
                            Phone_Number = PhoneNo,
                            Card_No = CardNo
                        };

                        var createdUserId = await ATMService.CreateUser(userData);

                        Console.WriteLine($"Created at ID no {createdUserId}");

                    }
                        break;

                    default:
                        goto here;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task EditAccountAsync()
        {
            AuthenticationOperation aTM = new AuthenticationOperation(new AtmDBContext());

            start:  Console.WriteLine("Enter CardNo of the account to edit");
            string CardNo = Console.ReadLine();

           Admin: Console.Write("VERIFY WHETHER YOUR AN ADMIN TO CONTINUE \n\tEnter your PinCode \n\t ===>");
            string PinCode = Console.ReadLine();

            switch (await AdminVerify(PinCode))
            {
                case true:
                    switch (await authentication.CheckUser(CardNo))
                    {
                        case true:
                            
                            using (IAtmDBServies ATMService = new DataBaseCRUD(new AtmDBContext()))
                            {
                                User editing = InputFormat.EditList(await aTM.GetUserDetails(CardNo));
                                var updateResult = await ATMService.UpdateUser(CardNo, editing);

                                if (updateResult)
                                {
                                    Console.WriteLine($"Successfully Updated");
                                }
                                else
                                {
                                    Console.WriteLine($"Not Successfully Updated");
                                }
                            }
                                break;
                        default:
                            Console.WriteLine($"{CardNo} is not in the database");
                            goto start;
		            }
                    break;
                default:
                    Console.WriteLine($"{PinCode} is incorrect");
                    goto Admin;
            }
            
        }

        public async Task DeleteAccount()
        {
           
        start: Console.Write("Enter CardNo of Account to be deleted\n ===>");
            string CardNo = Console.ReadLine();
           
            try
            {
               
                    Console.Write("VERIFY WHETHER YOUR AN ADMIN TO CONTINUE \n\tEnter your PinCode \n\t ===>");
                    string PinCode = Console.ReadLine();

                    switch (await AdminVerify(PinCode))
                    {
                        case true:
                            count = 4;
                            switch (await authentication.CheckUser(CardNo))
                            {
                                case true:
                                    User user = await authentication.GetUserDetails(CardNo);
                                    Admin admin = await GetAdminDetails(PinCode);
                                    switch (await data.DeleteUser(user.UserName))
                                    {
                                        case false:
                                            Console.WriteLine($"Account of {user.Last_name} {user.First_name} was not deleted from the database ");
                                            break;
                                        

                                        default:                                            
                                            Console.WriteLine($"Account of {user.Last_name} {user.First_name} was deleted from the database by {admin.Last_name} {admin.First_name} at {DateTime.Now.ToShortDateString()} by {DateTime.Now.ToShortTimeString()}");
                                            break;
                                    }

                                    break;
                                default:
                                    Console.WriteLine($"{CardNo} does not exist in the database");
                                    break;
                            }
                            break;
                        default:
                            count++;
                            Console.WriteLine($"Incorrect Pin, {3 - count}tries left");
                            break;
                    }
             
            
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto start;
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

