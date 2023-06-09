#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Car
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [MaxLength(20)]
        public string CarColor { get; set; }
        [MaxLength(20)]
        public string CarModel { get; set; }
        [MaxLength(20)]
        public string CarBrand { get; set; }
        [MaxLength(10)]
        public string CarLicensePlate { get; set; }
        [Column(TypeName = "smallint")]
        public int CarYear { get; set; }
        [MaxLength(20)]
        public string CarBodyType { get; set; }
        [MaxLength(20)]
        public string CarFuelType { get; set; }
        public int NumberOfCarLot {get; set;}
        [Column(TypeName = "tinyint")]
        public Status CarStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}