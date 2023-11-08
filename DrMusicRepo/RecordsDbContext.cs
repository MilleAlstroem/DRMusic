using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMusicRepo
{
    public class RecordsDbContext : DbContext
    {
        public RecordsDbContext(DbContextOptions<RecordsDbContext> options) : base(options) { }

        public DbSet<Record> Records { get; set; }
    }
}
