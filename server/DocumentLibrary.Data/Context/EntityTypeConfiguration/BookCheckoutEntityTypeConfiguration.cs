using DocumentLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentLibrary.Data.Context.EntityTypeConfiguration
{
    public class BookCheckoutEntityTypeConfiguration: IEntityTypeConfiguration<BookCheckout>
    {
        public void Configure(EntityTypeBuilder<BookCheckout> builder)
        {
            builder
                .Property(b => b.Id)
                .IsRequired();
            
            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCheckouts)
                .HasForeignKey(bc => bc.BookId)
                .IsRequired(true);
            
            builder
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(u => u.BookCheckouts)
                .HasForeignKey(bc => bc.ApplicationUserId)
                .IsRequired(true);
        }
    }
}