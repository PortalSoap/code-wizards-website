using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using code_wizards_website.Models;

namespace code_wizards_website.Data.Map
{
    public class AccountMap : IEntityTypeConfiguration<AccountViewModel>
    {
        public void Configure(EntityTypeBuilder<AccountViewModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);
        }
    }
}