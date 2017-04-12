using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FishingReports.Model;

namespace FishingReportsServices
{
    /// <summary>
    /// Service for processing login requests.
    /// </summary>
    [ServiceContract]
    public interface ILoginService
    {
        /// <summary>
        /// Verifies that the username and password are accepted.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool AttemptLogin(string username, string password);

        /// <summary>
        /// Checks to see if a username account already exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        bool DoesUserExist(string username);

        /// <summary>
        /// Inserts a user account.
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void InsertUser(UserEntity user);

        /// <summary>
        /// Finds a single user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        UserEntity GetUser(string username);

        /// <summary>
        /// Gets the total posts for a single user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        int GetNumberOfPosts(string username);

        /// <summary>
        /// Validates that a new user account is valid to create.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        IDictionary<string, string> ValidateNewUser(UserEntity user);
    }
}
