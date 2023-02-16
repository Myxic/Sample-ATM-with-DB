using System;
using System.Threading.Tasks;
using DataBase.Model;
using static DataBase.Model.User;

namespace Language.Model
{
    public interface ILanguageInterface
    {
        Task<User> VerficationAsync();
        void WelcomeMessage();
        Task Menu(User user);
        void Transfer();
        void Withdrawal();
        Task BalanceAsync(string UserName);
        void FailedTransation();
    }
}

