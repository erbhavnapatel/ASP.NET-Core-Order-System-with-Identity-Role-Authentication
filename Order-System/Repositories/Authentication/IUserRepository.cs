using Order_System.Identity;
using Order_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories.Authentication
{
    public interface IUserRepository
    {
        User GetUser(UserModel userModel);
    }
}
