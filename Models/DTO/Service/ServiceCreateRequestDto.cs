#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceCreateRequestDto
    {
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceDetailDescription { get; set; }
        public int ServiceDuration { get; set; }
    }
}