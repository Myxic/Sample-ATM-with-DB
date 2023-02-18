using Language;
using DataBase.Model;
using ATMOperations;
using DataBase;

namespace Impentation;
class Program
{
    static async Task  Main(string[] args)
    {
        await LanguageOption();
        
    }

    public static async Task LanguageOption()
    {
       start: Console.Write(@"Choose your Language: using the numbers
                   1: English
                   2: Chinese
                   3: Russian
==>  ");

        string? Input = Console.ReadLine();
        Console.Clear();
        switch (Input??" ")
        {
            case "1":
                EnglishImpentation english = new EnglishImpentation();
                User user = await english.VerficationAsync();
                Console.Clear();
                await english.Menu(user);
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
}

