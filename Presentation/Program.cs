using Language;
using DataBase.Model;
using ATMSystem;
using DataBase;
using static Presentation.MenuClass;

namespace Presentation;
class Program
{
    MenuClass Menu = new MenuClass();
     static async Task  Main(string[] args)
    {
       
        try
        {
            MenuClass menu = new MenuClass();
            await menu.MainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} at {ex.Source}, {ex.TargetSite}");
        }
        
    }

 
}

