#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Garage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GarageId { get; set; }
        [MaxLength(1024)]
        public string GarageName { get; set; }
        [MaxLength(500)]
        public string GarageAbout { get; set; }
        [MaxLength(1024)]
        public string GarageImage { get; set; }
        [MaxLength(1240)]
        public string GarageContactInformation { get; set; }
        // [MaxLength(20)]
        // public string FromTo { get; set; }
        [MaxLength(30)]
        public string OpenAt { get; set; }
        [MaxLength(30)]
        public string CloseAt { get; set; }
        [MaxLength(50)]
        public string GarageAddress { get; set; }
        [MaxLength(50)]
        public string GarageWard { get; set; }
        [MaxLength(30)]
        public string GarageDistrict { get; set; }
        [MaxLength(50)]
        public string GarageCity { get; set; }
        public double GarageLatitude { get; set; } = 0;
        public double GarageLongitude { get; set; } = 0;
        [Column(TypeName = "tinyint")]
        public Status GarageStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [Timestamp]
        public byte[] VersionNumber { get; set; }

        /*-------------------------------------------------*/
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<GarageDetail> GarageDetails { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<GarageMechanic> GarageMechanics { get; set; }
    }
}