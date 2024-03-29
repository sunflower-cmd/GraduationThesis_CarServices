namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailResponseDto
    {
        public int BookingId { get; set; }
        public bool WaitForAccept { get; set; }
        public string BookingTime { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string OriginalPrice { get; set; } = string.Empty;
        public string DiscountPrice { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public string FinalPrice { get; set; } = string.Empty;
        public GarageBookingDto? GarageBookingDto { get; set; }
        // public CarBookingDto CarBookingDto { get; set; }
        public CustomerBookingDto? CustomerBookingDto { get; set; }
        public List<BookingDetailDto>? BookingDetailDtos { get; set; }
    }
}