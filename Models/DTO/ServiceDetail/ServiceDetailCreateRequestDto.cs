#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceDetailCreateRequestDto
    {
        public float ServicePrice { get; set; }
        public int MinNumberOfCarLot { get; set; }
        public int MaxNumberOfCarLot { get; set; }
        public int ServiceId { get; set; }     

    }
}