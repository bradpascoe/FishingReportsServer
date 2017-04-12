using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishingReports.Model;

namespace FishingReports.DataAccess
{
    public interface ILoginDAL
    {
        bool DoesLoginExist(string username);

        bool AttemptLogin(string userName, string password);

        void InsertUser(UserEntity user);

        UserEntity QueryUser(string username);

        int QueryNumberOfPosts(string username);

        IDictionary<string, string> ValidateNewUser(UserEntity entity);
    }
}
