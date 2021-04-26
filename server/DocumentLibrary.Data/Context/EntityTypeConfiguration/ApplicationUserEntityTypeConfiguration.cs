using DocumentLibrary.Data.Entities;
using DocumentLibrary.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentLibrary.Data.Context.EntityTypeConfiguration
{
    public class ApplicationUserEntityTypeConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(b => b.SecondName)
                .HasMaxLength(100);
            
            builder
                .Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}