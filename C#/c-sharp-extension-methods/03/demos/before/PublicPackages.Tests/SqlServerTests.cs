using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace PublicPackages.Tests
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO - configure SQL Server
            optionsBuilder.UseSqlServer("TODO");
        }
    }

    public class SqlServerTests
    {
        [Test]
        public void UseSqlServer()
        {
            using (var context = new Context())
            { }
        }
    }
}
