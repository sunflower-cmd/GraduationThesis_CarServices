#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceListResponseDto
    {
        public int ServiceId { get; set; }        
        public string ServiceName { get; set; }
        // public string ServiceImage { get; set; }
        public int ServiceDuration { get; set; }
    }
}