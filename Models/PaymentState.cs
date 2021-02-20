using FiledCom.Enumerations;

namespace FiledCom.Models
{
    public class PaymentState
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public PaymentStatusTypes Status { get; set; }
    }
}