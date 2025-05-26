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
        //id עושה בעיות  להפוך לרגיל
        //private static int nextId=100;
        //לא עשיתי מחיקה בכאילו
        public int Id { get; set; }//אמור להתווסף לבד
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        //public User User { get; set; }//יתכן שמספיק רק userId לדעתי אפשר שיתקבל לפי הלוגין 
        public string UserId { get; set; }
        public Type type { get; set; }//יוכנס ע"י משתמש או לפי הלוגין
        public Hour()
        {
            
        }
        //public Hour(DateTime start, DateTime end)
        //{
        //    Id = nextId++;
        //    Start = start;
        //    End = end;
        //}
        public Hour(int id,DateTime start, DateTime end, string userId, Type type)
        {
            //Id = nextId++;
            Id = id;
            Start = start;
            End = end;
            UserId = userId;
            this.type = type;
        }
    }
}
