using MyProject.Core.DTOs;
using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAsync();
        Task<User> GetAsync(string id);
        Task PostAsync(User user);
        Task<User> PutAsync(string id, User user);
        Task<bool> DeleteAsync(string id);
    }
}
