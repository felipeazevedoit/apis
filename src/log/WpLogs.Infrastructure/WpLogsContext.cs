using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpLogs.Entities;

namespace WpLogs.Infrastructure
{
    public class WpLogsContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Logs;Trusted_Connection=True;Integrated Security=True;");
            //optionsBuilder.UseSqlServer(@"Data Source=18.229.17.132;Initial Catalog=Logs;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }
    }
}
