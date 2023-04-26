#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Payment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [MaxLength(20)]
        public string PaymentMethod { get; set; }
        [MaxLength(100)]
        public string PaymentMessage { get; set; }
        [MaxLength(5)]
        public string Currency { get; set; }
        [Column(TypeName = "tinyint")]
        public PaymentStatus PaymentStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

        /*-------------------------------------------------*/
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}