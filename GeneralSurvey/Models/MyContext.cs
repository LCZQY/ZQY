using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralSurvey.Models
{

    public class MyContext : DbContext
    {

        public DbSet<MySqlTest> mySqlTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;database=zqy;uid=root;pwd='root';");
            //optionsBuilder.UseMySQL("Data Source = localhost; Database = zqy; User ID = root; Password = 'root'; pooling = true; CharSet = utf8; port = 3306; sslmode = none;Convert Zero Datetime=True;Allow Zero Datetime=True;");
            base.OnConfiguring(optionsBuilder);
        }
        

    }
}
