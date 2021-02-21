using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardPaymentService.Api.Models;

namespace CreditCardPaymentService.Api.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> opt) : base(opt)
        {
            
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>(ConfigurePayment);
            modelBuilder.Entity<PaymentState>(ConfigurePaymentState);
        }

        private void ConfigurePayment(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.CreditCardNumber)                
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.CardHolder)                
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ExpirationDate)
                .HasColumnType("date");

            builder.Property(e => e.SecurityCode)                
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(9,2)");

        }

        private void ConfigurePaymentState(EntityTypeBuilder<PaymentState> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.PaymentId)
                .IsRequired();

            builder.HasOne<Payment>(s => s.Payment)
                .WithMany(t => t.PaymentStates)
                .HasForeignKey(s => s.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Status)
                .IsRequired();

       }
    }
}