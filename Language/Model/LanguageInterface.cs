using System;
using System.Threading.Tasks;
using DataBase.Model;
using static DataBase.Model.User;

namespace Language.Model
{
    public interface ILanguageInterface
    {
        Task<User> VerficationAsync();
        Task Menu(User user);
        Task<string> Transfer(User user);
        Task<string> Withdrawal(User user);
        Task BalanceAsync(string UserName);
    }
}

