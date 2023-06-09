using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IMechanicRepository
    {
        Task<List<Mechanic>> View(PageDto page);
        Task<List<Mechanic>> FilterMechanicsByGarageId(int garageId);
        Task<Mechanic?> Detail(int mechanicId);
        Task<bool> IsMechanicExist(int mechanicId);
        Task<List<WorkingSchedule>> FilterWorkingSchedulesByMechanicId(int mechanicId);
        Task<List<Mechanic>> FilterMechanicAvailableByGarageId(int garageId);
        Task Create(Mechanic mechanic);
        Task Update(Mechanic mechanic);
    }
}