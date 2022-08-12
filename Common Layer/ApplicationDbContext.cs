using Common_Layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserPersonalDetail> UserPersonalDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
