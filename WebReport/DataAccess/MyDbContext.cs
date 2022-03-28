using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReport.Models;

namespace WebReport.DataAccess
{
    public class MyDbContext : DbContext
    {
        public DbSet<Repost> Reposts { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}
