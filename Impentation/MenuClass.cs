using System;
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
                    await LanguageOption();
                    break;
                case "2":
                    AdminMenu();
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


        private void AdminMenu() 
	    {
           start: Console.Write("Enter Admin password\n ====> ");
            string? Input = Console.ReadLine();
            int count = 0;

            switch (Input)
            {
                case "Admin123456789":
                    Console.Clear();
                    
                    AdminOptions();
                    break;
       
                default:
                    int count1 = count+=1;
                    Console.WriteLine($"{Input} is invalid, {5 - count1} tries left");

                    if (count == 5) {
                        Environment.Exit(0);

		            }
                    goto start;
            }
        }

        private void AdminOptions()
        {
            start: Console.Write(@"Welcome to Admin space

1: Create / Add new account to the database
2: Update / Edit existing record
3: Delete existing record
4: To Exit");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"{input} is invalid");
                    goto start;
            }
        }

    }
}

