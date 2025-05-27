using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyProject.Core.entities;
using MyProject.Core.Interface;
using MyProject.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace MyProject.Service
{

    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;



        public AuthService(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<User> Registration(string userName, string password)
        {
            User user;
            if (userName.ToUpper() == "ADMIN" && password == "123456")//אתה מנהל
            {
                user = new User
                {
                    Id = "ADMIN",
                    Name = "Admin",
                    Email = "admin@admin.com",
                    Password = "123456",//צריך לשים??
                    Usertype = USERTYPE.ADMIN,
                    IsValid = true
                };
            }
            else
            {
                user = await Verify(userName, password);
            }
            return user;
        }
        public async Task<User> Verify(string name, string password)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(h => h.Name == name && h.Password == password && h.IsValid != false);
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
               // new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role, user.Usertype.ToString())

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}




































//public async Task<ActionResult> Registration(string userName, string password)
//{
//    User user;
//    if (userName.ToUpper() == "ADMIN" && password == "123456")//אתה מנהל
//    {
//        user = new User
//        {
//            Id = "ADMIN",
//            Name = "Admin",
//            Email = "admin@admin.com",
//            // Password="123456",//צריך לשים??
//            Usertype = USERTYPE.ADMIN
//        };
//    }
//    else
//    {
//        user = await _authService.Verify(userName, password);//צריך await?

//        if (user == null)//אם לא קיים כזה לקוח 
//        {
//            _logger.LogWarning($"wrong password");
//            return Unauthorized();
//        }
//    }
//    return Ok(GenerateToken(user));
//}

//public async Task<User> Verify(string name, string password)
//    {
//        return await _dataContext.Users.FirstOrDefaultAsync(h => h.Name == name && h.Password == password);
//    }
//    }
//}
