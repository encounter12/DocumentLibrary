using DocumentLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentLibrary.Data.Context.EntityTypeConfiguration
{
    public class KeywordEntityTypeConfiguration: IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(40);
            
            // Shadow foreign key - Add the shadow property to the model
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            builder.Property<long>("BookId");
            
            // Use the shadow property as a foreign key
            builder
                .HasOne(k => k.Book)
                .WithMany(b => b.Keywords)
                .HasForeignKey("BookId")
                .IsRequired();
        }
    }
}