using EmailService.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EmailService.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext()
        //{
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=EmailService;Integrated Security=SSPI;Application Name=EmailService");
        //}

        public DbSet<EmailTemplate> EmailTemplates { get; set; }
    }
}
