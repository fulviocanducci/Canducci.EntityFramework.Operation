using ConsoleAppTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppTest.Services
{
    public class Ctx : DbContext
    {
        public DbSet<Summary> Summary { get; set; }
        public Ctx(DbContextOptions<Ctx> options)
            : base(options)
        {
            
        }
    }
}
