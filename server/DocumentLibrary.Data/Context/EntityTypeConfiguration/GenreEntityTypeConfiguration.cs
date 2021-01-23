using DocumentLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentLibrary.Data.Context.EntityTypeConfiguration
{
    public class GenreEntityTypeConfiguration: IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}