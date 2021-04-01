using GlobalIMCTask.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Core.Contexts
{
    public class TaskContext : DbContext
    {
        private readonly string _dbPath;
        public TaskContext(string dbPath)
        {
            _dbPath = dbPath;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite("Data source=" + _dbPath);

        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<DietaryType> DietaryTypes { set; get; }

    }
}
