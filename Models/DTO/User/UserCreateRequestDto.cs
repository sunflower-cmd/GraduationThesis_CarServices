#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserCreateRequestDto
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }
}