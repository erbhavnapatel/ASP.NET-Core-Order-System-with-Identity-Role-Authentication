using Order_System.Identity;
using Order_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>();

        public UserRepository()
        {
            users.Add(new User
            {
                UserId = "a001",
                Password = "aman",
                Role = "manager"
            });
            users.Add(new User
            {
                UserId = "b001",
                Password = "bhavna",
                Role = "developer"
            });
            users.Add(new User
            {
                UserId = "c001",
                Password = "cathy",
                Role = "tester"
            });
            users.Add(new User
            {
                UserId = "d001",
                Password = "dixit",
                Role = "admin"
            });
            users.Add(new User
            {
                UserId = "e001",
                Password = "emily",
                Role = "admin"
            });
        }

        public User GetUser(UserModel userModel)
        {
            return users.Where(x => x.UserId == userModel.UserId
                && x.Password == userModel.Password).FirstOrDefault();
        }
    }
}
