#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceDetailServiceDto
    {
        public int ServiceDetailId { get; set; }
        public float ServicePrice { get; set; }
        public int MinNumberOfCarLot { get; set; }
        public int MaxNumberOfCarLot { get; set; }
    }
}