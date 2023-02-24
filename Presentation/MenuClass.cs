using System;
using System.ComponentModel;
using ATMSystem;
using DataBase;
using DataBase.Model;
using Language;


namespace Presentation
{
    public class MenuClass
    {
        EnglishImpentation english = new EnglishImpentation();


        public  async Task MainMenu()
        {
        start:
            Console.Write($@"Hello
1: User
2: Admin
=====> ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    await LanguageOption();
                    break;
                case "2":
                    Console.Clear();
                    await AdminMenuAsync();
                    break;
                default:
                    Console.WriteLine($"{input} is invalid");
                    goto start;
            }

        }

        private  async Task LanguageOption()
        {
        start: Console.Write(@"Choose your Language: using the numbers
                   1: English
                   2: Chinese
                   3: Russian
==>  ");

            string? Input = Console.ReadLine();
            Console.Clear();
            switch (Input ?? " ")
            {
                case "1":
                   
                    User user = await english.VerficationAsync();
                    Console.Clear();
                    await Menu(user);
                    break;
                case "2":
                    break;
                case "3":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{Input} is an Invalid Option\n");
                    goto start;
            }
        }

 

        private async Task Menu(User user)
        {
        start: Console.Write($@"Welcome {user.Last_name.ToUpper()} {user.First_name.ToUpper()}
1: View Balance
2: Transfer
3: Withdraw
4: Change Pin
0: Main Menu
#: Language Menu
0#: To Exit

=====> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    await english.BalanceAsync(user.UserName);
                    await ReturnToMenu(user);
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine(await english.Transfer(user));
                    await ReturnToMenu(user);

                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine(await english.Withdrawal(user));
                    await ReturnToMenu(user);
                    break;
                case "4":
                    Console.Clear();
                    await english.ChangePin(user);
                    await ReturnToMenu(user);
                    break;
                case "0":
                    Console.Clear();
                    await MainMenu();
                    await ReturnToMenu(user);
                    break;
                case "#":
                    Console.Clear();
                    await LanguageOption();
                    await ReturnToMenu(user);
                    break;
                case "0#":
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{input} is an invalid input");
                    goto start;
            }
        }

        private async Task ReturnToMenu(User user)
        {
            Console.Write("\n\n Enter \"0\" to return to Menu \n ===> ");
            string? input = Console.ReadLine();
            Console.Clear();
            await Menu(user);
        }


        private async Task AdminMenuAsync() 
	    {
            bool tries = true;
            int count = 0;

            while (tries)
            {
            start: Console.Write("Enter Admin password\n ====> ");
                string? Input = Console.ReadLine();
                switch (Input)
                {
                    case "Admin123456789":
                        Console.Clear();

                       await AdminOptionsAsync();
                        break;

                    default:
                        count++;
                        Console.WriteLine($"{Input} is invalid, {5 - count} tries left");

                        if (count == 5)
                        {
                            Console.Clear();
                            await MainMenu();
                        }
                        goto start;

                }

            }
            
        }

        private async Task AdminOptionsAsync()
        {
            AdminOperation admin = new AdminOperation(new AtmDBContext());
        start: Console.Write(@"Welcome to Admin space

1: Create / Add new record to the database
2: Update / Edit existing record
3: Delete existing record
0: To Exit
===> ");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    await admin.CreateAccountAsync();
                    await ReturnToMenu();
                    break;
                case "2":
                    await ReturnToMenu();
                    break;
                case "3":
                    await admin.DeleteAccount();
                    await ReturnToMenu();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"{input} is invalid");
                    goto start;
            }
        }

        private async Task ReturnToMenu()
        {
            start: Console.Write("\n\n Enter \"0\" to return to MainMenu \nEnter \"#\" to return to Admin Menu\n ===> ");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    await MainMenu();
                    break;
                case "#":
                    await AdminOptionsAsync();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{input} is an invalid command");
                    goto start;
            }
            
        }

    }
}

