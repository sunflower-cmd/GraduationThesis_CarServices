using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserStatusRequestDto
    {
        public int UserId { get; set; }
        public Status UserStatus { get; set; }
    }
}