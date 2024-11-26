using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NSIDictionaryService.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseSqlServer("Data Source=DESKTOP-H2G7L70\\SQLEXPRESS;Initial Catalog=NSIDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
            return new ApplicationContext(builder.Options);
        }
    }
}
