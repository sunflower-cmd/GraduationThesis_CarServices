using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingRepository
    {
        Task<List<Booking>?> View(PageDto page);
        Task<Booking?> Detail(int id);
        Task<int> Create(Booking booking);
        Task Update(Booking booking);
        Task<List<Booking>?> FilterBookingByDate(DateTime dateSelect, int garageId);
        Task<List<Booking>?> FilterBookingByTimePerDay(DateTime dateTime, int garageId);
        Task<bool> IsBookingExist(int bookingId);
        Task<List<Booking>?> FilterBookingByGarageId(int garageId, PageDto page);
    }
}