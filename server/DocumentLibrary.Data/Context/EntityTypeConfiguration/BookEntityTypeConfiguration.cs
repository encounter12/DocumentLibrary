using System;
using DocumentLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentLibrary.Data.Context.EntityTypeConfiguration
{
    public class BookEntityTypeConfiguration: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(b => b.Id)
                .UseHiLo();

            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            builder
                .Property(b => b.Description)
                .HasMaxLength(4000);

            // Shadow foreign key - Add the shadow property to the model
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            builder.Property<long>("GenreId");
            
            // Use the shadow property as a foreign key
            builder
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey("GenreId")
                .IsRequired();
        }
    }
}