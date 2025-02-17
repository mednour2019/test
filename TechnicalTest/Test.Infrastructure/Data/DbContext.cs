using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Entities;

namespace Test.Infrastructure.Data
{
   public class DbContextTechnicalTest:DbContext
   {
        public DbContextTechnicalTest(DbContextOptions<DbContextTechnicalTest> options):
            base(options){ }
        public DbSet<User> Users { get; set; }
            
   }
    
}
