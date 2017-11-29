using System.Data.Entity.Migrations;

namespace MyPersonalDiary.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyPersonalDiary.Domain.Concrete.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyPersonalDiary.Domain.Concrete.EfDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
