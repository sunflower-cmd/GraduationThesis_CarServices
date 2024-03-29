using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateRequestDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        [DefaultValue("MM/dd/yyyy")]
        public string DateSelected { get; set; } = string.Empty;
        [DefaultValue("hh:mm:ss")]
        public string TimeSelected { get; set; } = string.Empty;
        
        public List<int>? ServiceList { get; set; } 

        public int MechanicId { get; set; }
        public int CarId { get; set; }
        public int GarageId { get; set; }
        public int CouponId { get; set; }

        //public byte[] VersionNumber { get; set; }
    }
}