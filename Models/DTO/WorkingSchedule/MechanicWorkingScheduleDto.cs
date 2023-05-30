#nullable disable
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.User;

namespace GraduationThesis_CarServices.Models.DTO.WorkingSchedule
{
    public class MechanicWorkingScheduleDto
    {
        public int MechanicId { get; set; }
        public UserDetailResponseDto UserDetailResponseDto { get; set; }

    }
}