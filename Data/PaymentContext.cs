using Microsoft.EntityFrameworkCore;
using FiledCom.Models;

namespace FiledCom.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> opt) : base(opt)
        {
            
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }
    }
}