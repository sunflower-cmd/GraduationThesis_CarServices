using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IMechanicRepository
    {
        Task<(List<Mechanic>, int, List<int>)> View(PageDto page);
        Task<List<Mechanic>> FilterMechanicsByGarage(int garageId);
        Task<List<Mechanic>> FilterMechanicsAvailableByGarage(int garageId);
        Task<List<Mechanic>> GetMechanicByBooking(int bookingId);
        Task<Mechanic?> Detail(int mechanicId);
        Task<bool> IsMechanicExist(int mechanicId);
        // Task<List<WorkingSchedule>> FilterWorkingSchedulesByMechanicId(int mechanicId);
        Task<List<Mechanic>> FilterMechanicAvailableByGarageId(int garageId);
        Task Create(Mechanic mechanic);
        Task Update(Mechanic mechanic);
        Task CreateBookingMechanic(BookingMechanic bookingMechanic);
        Task<BookingMechanic?> IsCustomerPickMainMechanic(DateTime date);
    }
}