using System;
using DataBase.Model;
using Language.Model;
//using ATMOperations.Authentication;

namespace Language
{
    public class IEnglishImpentation : ILanguageInterface
    {
        public IEnglishImpentation()
        {
        }
        public void BalanceQuestion()
        {
            throw new NotImplementedException();
        }

        public void FailedTransation()
        {
            throw new NotImplementedException();
        }

       

        public void Menu(User user)
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

        public void TransferQuestion()
        {
            throw new NotImplementedException();
        }

        public void Verfication()
        {
            Console.Write($"Enter your Card Number\n ====>");
            string CardNo = Console.ReadLine();
            //Authentication.CheckUser(CardNo);


        }

        public void WelcomeMessage()
        {
            throw new NotImplementedException();
        }

        public void WithdrawalQuestion()
        {
            throw new NotImplementedException();
        }
    }
}

