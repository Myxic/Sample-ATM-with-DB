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
        void Menu(User user);
        void TransferQuestion();
        void WithdrawalQuestion();
        void BalanceQuestion();
        void FailedTransation();
    }
}

