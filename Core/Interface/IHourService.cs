using MyProject.Core.DTOs;
using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Interface
{
    public interface IHourService
    {
        Task<List<HourDTO>> GetAsync();
        Task<List<Hour>> GetByUserId(string id);
        Task PostAsync(Hour hour);
        Task<Hour> PutAsync(string id, Hour hour);
        Task<bool> DeleteAsync(string id);
    }
}
