using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement.EF;

namespace LibraryManagement.Auth
{
    public class UserManager
    {
        private readonly LibraryManagementEntities _dbContext = new LibraryManagementEntities();

        public User GetUser(string userEmail, string password)
        {
            // Fetch the user directly from the database
            return _dbContext.Users.FirstOrDefault(u => u.UserEmail == userEmail && u.Password == password);
        }

        public bool IsEmailUnique(string email)
        {
            // Check if the email exists in the database
            return !_dbContext.Users.Any(u => u.UserEmail == email);
        }

        public void AddUser(User user)
        {
            // Add the user to the database
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}