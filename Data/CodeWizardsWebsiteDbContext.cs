using Microsoft.EntityFrameworkCore;
using code_wizards_website.Models;
using code_wizards_website.Data.Map;

namespace code_wizards_website.Data
{
    public class CodeWizardsWebsiteDbContext : DbContext
    {
        public CodeWizardsWebsiteDbContext(DbContextOptions<CodeWizardsWebsiteDbContext> options) 
        : base(options)
        {
            
        }

        public DbSet<AccountViewModel> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}