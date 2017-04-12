using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using FishingReports.Model;

namespace FishingReports.DataAccess
{
    public class LoginDAL : ILoginDAL
    {
        internal LoginDAL()
            : this(new ConnectionStringProvider())
        {
        }

        internal LoginDAL(ConnectionStringProvider provider)
        {
            mConnectionProvider = provider;
        }

        private ConnectionStringProvider mConnectionProvider;

        public bool DoesLoginExist(string username)
        {
            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
               mConnectionProvider.GetConnectionString()))
            {
                User user = (from users in context.Users
                             where users.UserName == username
                             select users).SingleOrDefault();

                return user != null;
            }
        }

        public bool AttemptLogin(string userName, string password)
        {
            bool successful = false;

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mConnectionProvider.GetConnectionString()))
            {
                var userNameRecord = (from UserNames in context.Users
                                      where UserNames.UserName == userName && UserNames.Password == password
                                      select UserNames).FirstOrDefault();

                successful = userNameRecord != null;
            }

            return successful;
        }

        public void InsertUser(UserEntity user)
        {
            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mConnectionProvider.GetConnectionString()))
            {
                context.sp_InsertUser(user.Username, user.Password, user.Email, user.Residence);
            }
        }

        public UserEntity QueryUser(string username)
        {
            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
               mConnectionProvider.GetConnectionString()))
            {
                User user = (from users in context.Users
                             where users.UserName == username
                             select users).SingleOrDefault();

	            return user != null
		            ? new UserEntity
		            {
			            Username = user.UserName,
			            Password = user.Password,
			            Password2 = user.Password,
			            Residence = user.Residence,
			            Email = user.Email
		            }
		            : null;
            }
        }

        public IDictionary<string, string> ValidateNewUser(UserEntity user)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (user.Username.Trim().Length < 6 || user.Username.Trim().Length > 16)
            {
                errors.Add("Username", "Username must be between 6 and 16 characters long.");
            }

            if (user.Password.Trim() != user.Password.Trim())
            {
                errors.Add("Password", "Passwords must match");
            }

            if (user.Password.Trim().Length < 6 || user.Password.Trim().Length > 10)
            {
                errors.Add("Password", "Password must be between 6 and 10 characters long.");
            }

            if (QueryUser(user.Username) != null)
            {
                errors.Add("Username", "Username already exists.");
            }

            return errors;
        }

        public int QueryNumberOfPosts(string username)
        {
            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
               mConnectionProvider.GetConnectionString()))
            {
                int count = (from reports in context.Reports
                             where reports.UserName == username
                             select reports).Count();

                return count;
            }
        }
    }
}