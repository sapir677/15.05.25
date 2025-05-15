using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.entities
{
    public enum USERTYPE
    {
        ADMIN, EMPLOYEE
    }
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public USERTYPE Usertype { get; set; }
        public User() { }
        public User(string id, string name, string email, string password, USERTYPE usertype)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Usertype = usertype;
        }

        public override string ToString()
        {
            return "User type: "+ Usertype+"\nname:  " +Name + "\nid: " + Id + "\nemail: " + Email + "\npassword: " + Password;
        }

    }
}
