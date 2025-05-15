using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public USERTYPE Usertype { get; set; }
    }
}
