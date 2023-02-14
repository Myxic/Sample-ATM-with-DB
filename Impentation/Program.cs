namespace Impentation;
class Program
{
    static void Main(string[] args)
    {
        LanguageOption();
    }
    public static void LanguageOption()
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

