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
        public async Task BalanceAsync(string UserName)
        {
            DataBaseCRUD Userdata = new DataBaseCRUD(new AtmDBContext());
            var user = await Userdata.GetUser(UserName);
            Console.WriteLine(user.Balance);
        }

        public void FailedTransation()
        {
            throw new NotImplementedException();
        }

       

        public async Task Menu(User user)
        {
        start: Console.Write($@"Welcome {user.Last_name.ToUpper()} {user.First_name.ToUpper()}
1: View Balance
2: Transfer
3: Withdraw
4: Change Pin
=====>");
            
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await BalanceAsync(user.UserName);
                    break;
                case "2":
                    Console.WriteLine(await Transfer(user));

                    break;
                case "3":
                    Console.WriteLine(await Withdrawal(user));
                    break;
                case "4":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{input} is an invalid input");
                    goto start;
            }
        }

        public async Task<string> Transfer(User user)
        {
        start:    Console.Write("Enter Amount to Transfer\n ====>");
            string Amount = Console.ReadLine();

            try
            {
                decimal CashTransfered = Convert.ToDecimal(Amount);
                
                Console.WriteLine("Enter UserName of the person you want to transfer to");
                var receiver = Console.ReadLine();
                User user1 = await Operation.GetUserDetails(receiver);
                switch (await Operation.UpdateDB(user, Operation.Transation(Operation.GetUserBalance(user), user)))
                {
                    case true:
                       await Operation.UpdateReceiver(user1, CashTransfered);
                        return $"Tranfer of {CashTransfered} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} to {user1.Last_name} {user1.First_name} was successful\n New Balance is {user.Balance}";
                    default:
                        return $"Tranfer of {CashTransfered} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} to {user1.Last_name} {user1.First_name} was unsuccessful\n New Balance is {user.Balance}";


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                goto start;
                
            }
        }

        public  async Task<User> VerficationAsync()
        {
            AuthenticationOperation login = new AuthenticationOperation(new AtmDBContext());
            start:  Console.Write($"Enter your Card Number\n ====>");
            string CardNo = Console.ReadLine();
               
            switch (await login.CheckUser(CardNo))
            {
                case true:
                    Console.Write(($"Enter your Card Pin\n ====>"));
                    string Pin = Console.ReadLine();
                    switch (await login.CheckPin(Pin, CardNo))
                    {
                        case true:
                            return await login.GetUserDetails(CardNo);
                        default:
                            break;
                    }
                    return await login.GetUserDetails(CardNo);
                default:
                    //Console.Clear();
                    Console.WriteLine(await login.CheckUser(CardNo));
                    Console.WriteLine($"{CardNo} is an invalid Card Number");
                    goto start;
                    
            }

        }

        public void WelcomeMessage()
        {
            
        }

        public async Task<string> Withdrawal(User user)
        {
            Console.Write("Enter Amount to withdraw\n ====>");
            string Amount = Console.ReadLine();

            try
            {
                decimal CashWithdrawal = Convert.ToDecimal(Amount);

                
                switch (await Operation.UpdateDB(user, Operation.Transation(Operation.GetUserBalance(user), user)))
                {
                    case true:
                        return $"Withdrawal of {CashWithdrawal} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} was successful\n New Balance is {user.Balance}";
                    default:
                        return $"Withdrawal of {CashWithdrawal} from {user.Last_name.ToUpper()} {user.First_name.ToUpper()} was unsuccessful\n  Balance is {user.Balance}";


                }

            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }
        }
    }
}

