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
        //return Task.CompletedTask;
    }

    public static async Task LanguageOption()
    {
       start: Console.Write(@"Choose your Language: using the numbers
                   1: English
                   2: Chinese
                   3: Russian
==>  ");

        string? Input = Console.ReadLine();

        switch (Input??" ")
        {
            case "1":
                EnglishImpentation english = new EnglishImpentation();
                await english.VerficationAsync();
                //AuthenticationOperation hey = new AuthenticationOperation(new AtmDBContext());
                //var isUserValid = await hey.CheckUser("372301558363216");
                //Console.WriteLine(isUserValid.ToString());
                //var heyhgf = Console.ReadLine();
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

