using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProject.Core.DTOs;
using MyProject.Core.entities;
using MyProject.Core.Interface;
using MyProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> GetAsync()
        {
            var users = await _dataContext.Users.ToListAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<User> GetAsync(string id) => await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        public async Task PostAsync(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<User> PutAsync(string id, User user)
        {
            User u = _dataContext.Users.ToList().Find(u => u.Id == id);
            if (u != null)
            {
                u.Id = user.Id;
                u.Name = user.Name;
                u.Email = user.Email;
                u.Password = user.Password;
                u.Usertype = user.Usertype;
                await _dataContext.SaveChangesAsync();
            }
            return u;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            User u = await GetAsync(id);
            if (u == null)
                return false;
            _dataContext.Users.Remove(u);
            await _dataContext.SaveChangesAsync();
            return true;
        }

    }
}
