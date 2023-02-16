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
        start: Console.Write($@"Welcome {user.Last_name.ToUpper()} {user.First_name.ToUpper()}\n
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
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{input} is an invalid input");
                    goto start;
            }
        }

        public void Transfer()
        {
            throw new NotImplementedException();
        }

        public  async Task<User> VerficationAsync()
        {
            AuthenticationOperation login = new AuthenticationOperation(new AtmDBContext());
            start:  Console.Write($"Enter your Card Number\n ====>");
            string CardNo = Console.ReadLine();
        
           
            //string CheckUser = CheckingUser.ToString();
            //Console.WriteLine(CheckUser);
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
                    Console.Clear();
                    Console.WriteLine($"{CardNo} is an invalid Card Number");
                    goto start;
                    
            }

        }

        public void WelcomeMessage()
        {
            
        }

        public void Withdrawal()
        {
            throw new NotImplementedException();
        }
    }
}

