using System;
using DataBase.Model;
using static DataBase.Model.User;

namespace Language.Model
{
    public interface ILanguageInterface
    {
        void VerficationAsync();
        void WelcomeMessage();
        void Menu(User user);
        void TransferQuestion();
        void WithdrawalQuestion();
        void BalanceQuestion();
        void FailedTransation();
    }
}

