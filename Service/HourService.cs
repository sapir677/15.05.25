using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class HourService : IHourService
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public HourService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<HourDTO>> GetAsync()
        {
            var hours= await _dataContext.Hours.ToListAsync();
            return _mapper.Map<List<HourDTO>>(hours);
        }

        ///public async Task<User> GetAsync(int id) => await _dataContext.Hours.FirstOrDefaultAsync(h => h.Id == id);

        public async Task<List<Hour>> GetByUserId(string id)
        {
            List<Hour> hours = await _dataContext.Hours.ToListAsync();
            if (hours==null)
                return null;
            return hours.FindAll(h =>h.UserId == id);
        }
        public async Task<List<Hour>> GetByDate(DateTime date)
        {
            List<Hour> hours = await _dataContext.Hours.ToListAsync();
            if (hours == null)
                return null;
            return hours.FindAll(h => h.Start.Equals(date));
        }
        public async Task PostAsync(Hour hour)
        {
            _dataContext.Hours.Add(hour);
            await _dataContext.SaveChangesAsync();

        }
        ////נסיון ההוא שמעל היה קודם
        //public async Task PostAsync(DateTime start,DateTime end)
        //{
        //    _dataContext.Hours.Add(new User(start,end));
        //    await _dataContext.SaveChangesAsync();
        //}

        public async Task<Hour> PutAsync(string id, Hour hour)
        {
            var h = await _dataContext.Hours.LastOrDefaultAsync(h => h.UserId == id);//התאריך האחרון שהתקבל הוא זה שישלח לשינוי 
            if (h != null)
            {
                //h.Id=hour.id;
                h.Start = hour.Start;
                h.End = hour.End;
                h.UserId = h.UserId;
            }
            return h;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            List<Hour> hour =await GetByUserId(id);
            if (hour== null)
                return false;
            _dataContext.Hours.Remove(hour.Last());//אין ככ הגיון
            await _dataContext.SaveChangesAsync();
            return true;
        }

    }
}
