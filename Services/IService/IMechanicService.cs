using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IMechanicService
    {
        Task<GenericObject<List<MechanicListResponseDto>>> View(PageDto page);
        Task<GenericObject<List<MechanicListResponseDto>>> FilterMechanicsByGarage(PagingBookingPerGarageRequestDto requestDto);
        Task<List<MechanicListResponseDto>> FilterMechanicsByBooking(int bookingId);
        Task<MechanicDetailResponseDto?> Detail(int mechanicId);
        Task<List<MechanicWorkForBookingResponseDto>> GetMechanicByBooking(int bookingId);
        Task<List<MechanicWorkForGarageResponseDto>> GetMechanicByGarage(int garageId);
        // Task<List<WorkingScheduleListResponseDto>> FilterWorkingSchedulesByMechanicId(int mechanicId);
        Task AddAvaliableMechanicToBooking(EditMechanicBookingRequestDto requestDto);
        Task RemoveMechanicfromBooking(EditMechanicBookingRequestDto requestDto);
        Task<GenericObject<List<BookingListResponseDto>>> GetBookingMechanicApplied(FilterBookingByMechanicRequestDto requestDto);
        Task<List<MechanicWorkForGarageResponseDto>> GetMechanicAvaliableByGarage(int garageId);
    }
}