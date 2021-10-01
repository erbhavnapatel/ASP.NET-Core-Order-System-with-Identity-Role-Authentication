using Order_System.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories.Authentication
{
    public interface ITokenRepository
    {
        string BuildToken(string key, string issuer, User user);
        bool ValidateToken(string key, string issuer, string token);
    }
}
