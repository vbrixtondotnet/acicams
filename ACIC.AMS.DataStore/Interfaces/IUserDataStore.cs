using ACIC.AMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IUserDataStore 
    {
        Dto.User GetUserById(int id);
        List<Dto.User> GetUsers(bool deleted = false);
        Dto.User Login(string username, string password);
        Dto.User SaveUser(Dto.User user);
        bool SetActive(int id, bool active);
        bool DeleteUser(int id);
    }
}
