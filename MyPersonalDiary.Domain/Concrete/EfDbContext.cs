using System.Data.Entity;
using MyPersonalDiary.Domain.Entities;

namespace MyPersonalDiary.Domain.Concrete
{
   public class EfDbContext:DbContext
    {
        public EfDbContext()
        {}
        public EfDbContext(string conn):base(conn){ }
        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
