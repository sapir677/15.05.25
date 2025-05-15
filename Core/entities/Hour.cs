using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.entities
{
    public enum Type
    {
       PRESENCE,FREEDOM,DISEASE
    }
    public class Hour
    {
        private static int nextId=100;
        public int Id { get;}//אמור להתווסף לבד
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public User User { get; set; }//יתכן שמספיק רק userId לדעתי אפשר שיתקבל לפי הלוגין 
        public Type type { get; set; }//יוכנס ע"י משתמש או לפי הלוגין
        public Hour()
        {
            
        }
        public Hour(DateTime start, DateTime end)
        {
            Id = nextId++;
            Start = start;
            End = end;
        }
        public Hour(DateTime start, DateTime end, User user, Type type)
        {
            Id = nextId++;
            Start = start;
            End = end;
            User = user;
            this.type = type;
        }
    }
}
