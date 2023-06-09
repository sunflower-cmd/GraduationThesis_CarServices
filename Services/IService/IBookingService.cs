using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<List<BookingListResponseDto>?> View(PageDto page);
        Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto);
        Task<BookingDetailResponseDto?> Detail(int id);
        Task Create(BookingCreateRequestDto requestDto);
        Task UpdateStatus(BookingStatusRequestDto requestDto);
        Task<List<BookingListResponseDto>?> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto);
    }
}