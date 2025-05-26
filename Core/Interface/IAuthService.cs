using Microsoft.IdentityModel.Tokens;
using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace MyProject.Core.Interface
{
    public interface IAuthService
    {
        Task<User> Verify(string name, string password);
        Task<User> Registration(string userName, string password);
        string GenerateToken(User user);
    }
}

