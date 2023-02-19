using System;
using DataBase.Model;
using Language.Model;
using ATMOperations;
using System.Threading.Tasks;
using DataBase;

namespace Language
{
    public class EnglishImpentation : ILanguageInterface
    {
        private readonly object Authentication;
        TransferOperation Operation = new TransferOperation(new AtmDBContext());
        public EnglishImpentation()
        {
        }

        public async Task Menu(User user)
        {
        start: Console.Write($@"Welcome {user.Last_name.ToUpper()} {user.First_name.ToUpper()}
1: View Balance
2: Transfer
3: Withdraw
4: Change Pin
=====> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await BalanceAsync(user.UserName);
                    await ReturnToMenu(user);
                    break;
                case "2":
                    Console.WriteLine(await Transfer(user));
                    await ReturnToMenu(user);

                    break;
                case "3":
                    Console.WriteLine(await Withdrawal(user));
                    await ReturnToMenu(user);
                    break;
                case "4":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{input} is an invalid input");
                    goto start;
            }
        }


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
                            System.Console.WriteLine(await login.GetUserDetails(CardNo) );
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
        private async Task ReturnToMenu(User user)
        {
            Console.Write("\n\n Enter \" 0 \" to return to Menu \n ===> ");
            string input = Console.ReadLine();
            Console.Clear();
            await Menu(user);
        }
    }
}

