using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FishingReports.Model;
using FishingReports.DataAccess;

namespace FishingReportsServices
{
    /// <summary>
    /// Service for processing login information.
    /// </summary>
    public class LoginService : ILoginService
    {
        public LoginService()
        {
            mDataAccess = DALFactory.CreateLoginDAL();
        }

        private ILoginDAL mDataAccess;

        /// <summary>
        /// Verifies that the username and password are accepted.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AttemptLogin(string username, string password)
        {
            return mDataAccess.AttemptLogin(username, password);
        }

        /// <summary>
        /// Inserts a user account.
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(UserEntity user)
        {
            mDataAccess.InsertUser(user);
        }

        /// <summary>
        /// Finds a single user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserEntity GetUser(string username)
        {
            return mDataAccess.QueryUser(username);
        }

        /// <summary>
        /// Validates a new user account.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDictionary<string, string> ValidateNewUser(UserEntity user)
        {
            return mDataAccess.ValidateNewUser(user);
        }

        /// <summary>
        /// Gets the total posts for a single user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetNumberOfPosts(string username)
        {
            return mDataAccess.QueryNumberOfPosts(username);
        }

        /// <summary>
        /// Checks to see if a username account already exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DoesUserExist(string username)
        {
            return mDataAccess.DoesLoginExist(username);
        }
    }
}
