﻿using AutoMapper;
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
            users = users.FindAll(u => u.IsValid != false); // Filter out invalid users
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<User> GetAsync(string id) => await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsValid != false);
        public async Task<User> PostAsync(User user)
        {
            User u = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);//אם קיים כזה משתמש
            if (u != null)//קיים משתמש
                if (u.IsValid != false)
                    return null;
                else
                {
                    _dataContext.Users.Remove(u);
                    await _dataContext.SaveChangesAsync();
                }
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> PutAsync(string id, User user)
        {
            User u = _dataContext.Users.ToList().FirstOrDefault(u => u.Id == id);
            if (u != null && u.IsValid != false)
            {
                u.Name = user.Name;
                u.Email = user.Email;
                u.Password = user.Password;
                await _dataContext.SaveChangesAsync();
            }
            return u;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            User u = await GetAsync(id);
            if (u == null)
                return false;
            u.IsValid = false; // Instead of removing, we mark the user as invalid
            _dataContext.Users.Update(u);
            await _dataContext.SaveChangesAsync();
            return true;
        }


    }
}
