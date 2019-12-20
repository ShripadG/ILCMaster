using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeservice.Models
{
    public partial class IlcMasterDbContext : DbContext
    {
        public IlcMasterDbContext()
        {
        }

        public IlcMasterDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<IlcMaster> Employees { get; set; }
    }
}
