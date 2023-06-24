using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<GenericObject<List<BookingListResponseDto>>> View(PageDto page);
        Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto);
        Task<BookingDetailResponseDto?> Detail(int id);
        Task Create(BookingCreateRequestDto requestDto);
        Task UpdateStatus(int bookingId, BookingStatus bookingStatus);
        Task<List<BookingListResponseDto>?> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto);
        Task<String> GenerateQRCode(int bookingId);
        Task<List<FilterByCustomerResponseDto>?> FilterBoookingByCustomer(FilterByCustomerRequestDto requestDto);
        Task RunQRCode(int bookingId);
    }
}