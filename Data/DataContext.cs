using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyProject.Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyProject.Data
{
    public class DataContext : DbContext
    {

        //entity framework
        //אמור להיות רשימת שעות  עובדים \משתמשים מסוג  dbset
        public DbSet<User> Users {  get; set; }
        public DbSet<Hour> Hours { get; set; }

        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }


        //(localdb)\MSSQLLocalDB

        //מהמורה clean
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        //    // .LogTo(Console.WriteLine, LogLevel.Information);
        //}



        //משלי
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        //}


        //entity מהקישור מהמורה שמה
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; ";
        //        //"Data Source = (local); Initial Catalog = SamuraiApp; Integrated Security = True;";
        //    //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyWorldDB;Integrated Security=True;Connect Timeout=30

        //    optionsBuilder.UseSqlServer(connectionString)
        //        .LogTo(Console.WriteLine,
        //                new[] {
        //                    DbLoggerCategory.Database.Command.Name,
        //                }
        //                , LogLevel.Information
        //                )
        //        .EnableSensitiveDataLogging();
        //}


    }
}
