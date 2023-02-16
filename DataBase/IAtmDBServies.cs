using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Model;
using static DataBase.Model.User;

namespace DataBase
{
    public interface IAtmDBServies : IDisposable
    {
        Task<long> CreateUser(User user);

        Task<bool> UpdateUser(string UserName, User user);

        Task<bool> DeleteUser(int id);

        Task<User> GetUser(string UserName);

        Task<IEnumerable<User>> GetUsers();
    }
  
}