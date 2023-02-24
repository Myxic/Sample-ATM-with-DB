using System;
using DataBase.Model;
using Language.Model;
using ATMSystem;
using System.Threading.Tasks;
using DataBase;


namespace Language
{
    public class EnglishImpentation : ILanguageInterface
    {
        //private readonly object? Authentication;
        ATMUsersOperations Operation = new ATMUsersOperations(new AtmDBContext());
        AuthenticationOperation login = new AuthenticationOperation(new AtmDBContext());
        

          public  async Task<User> VerficationAsync()
        {
            AuthenticationOperation login = new AuthenticationOperation(new AtmDBContext());
            start:  Console.Write($"Enter your Card Number\n ====> ");
            string CardNo = Console.ReadLine();
            

            switch (await login.CheckUser(CardNo))
            {
                case true:
                    Console.Write(($"Enter your Card Pin\n ====> "));
                    string Pin = Console.ReadLine();
                    switch (await login.CheckPin(Pin, CardNo))
                    {
                        case true:
                        try
                        {
                             return await login.GetUserDetails(CardNo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine( ex.Message);
                            goto start;
                        }
                           
                        default:
                            goto start;
                    }
                    
                default:
                    Console.Clear();
                    Console.WriteLine($"{CardNo} is an invalid Card Number");
                    goto start;
                    
            }

        }


        public async Task BalanceAsync(string UserName)
        {
            Console.Clear();
            try
            {
                DataBaseCRUD Userdata = new DataBaseCRUD(new AtmDBContext());
                User user = await Userdata.GetUser(UserName);
                decimal Balance = Operation.GetUserBalance(user);
                Console.WriteLine($"Your Balance is ₦{Balance}.");
               
            }
            catch (Exception ex)
            {
                Console.Clear();
               
                Console.WriteLine($"{ex.Message}" );
            }
           
        }


       
        public async Task<string> Transfer(User user)
        {
            Console.Clear();
        start:    Console.Write("Enter Amount to Transfer\n ====> ");
            string Amount = Console.ReadLine();

            try
            {
                decimal CashTransfered = Convert.ToDecimal(Amount);
                
                Console.Write("Enter UserName of the person you want to transfer to \n ==> ");

                var receiver = Console.ReadLine();

                User user1 = await Operation.GetUserDetails(receiver);

                switch (await Operation.UpdateDB(user, Operation.Transation (CashTransfered, user)))
                {
                    case true:
                       await Operation.UpdateReceiver(user1, CashTransfered);
                        return $"Transfer of ₦{CashTransfered} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} to {user1.Last_name} {user1.First_name} was successful\n New Balance is  ₦{(await Operation.GetUserDetails(user.UserName)).Balance}";
                    default:
                        return $"Transfer of ₦{CashTransfered} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} to {user1.Last_name} {user1.First_name} was unsuccessful\n New Balance is  ₦{(await Operation.GetUserDetails(user.UserName)).Balance}";



                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                goto start;
                
            }
        }

      
       
        public async Task<string> Withdrawal(User user)
        {
            Console.Clear();
            Console.Write("Enter Amount to withdraw\n ====> ");
            string Amount = Console.ReadLine();

            try
            {
                decimal CashWithdrawal = Convert.ToDecimal(Amount);

                
                switch (await Operation.UpdateDB(user, Operation.Transation(CashWithdrawal, user)))
                {
                    case true:
                        return $"Withdrawal of ₦{CashWithdrawal} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} was successful\n New Balance is  ₦{(await Operation.GetUserDetails(user.UserName)).Balance}";
                    default:
                        return $"Withdrawal of ₦{CashWithdrawal} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} was unsuccessful\n  Balance is  ₦{(await Operation.GetUserDetails(user.UserName)).Balance}";


                }

            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }
       

        public async Task ChangePin(User user)
        {
            start: Console.Write(($"Enter your Current Card Pin\n ====> "));
            string Pin = Console.ReadLine();
            switch (await login.CheckPin(Pin, user.Card_No))
            {
               
                case true:
                    try
                    { 
                         Console.Write(($"Enter your New Card Pin\n ====> "));
                         string NewPin = Console.ReadLine();
                    
                        switch(await Operation.UpdatePinCode(user, NewPin))
                        {
                            case true:
                                Console.WriteLine($"Change of PinCode of Account belonging to {user.Last_name} {user.First_name} from {Pin} to {(await login.GetUserDetails(user.Card_No)).Pin_No} was successful");
                                break;
                            default:
                                Console.WriteLine($"Change of PinCode of Account belonging to {user.Last_name} {user.First_name} from {Pin} to {NewPin} was unsuccessful");
                                break;
			            }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto start;
                    }
            
                default:
                    goto start;
            }
        }
    }
}

