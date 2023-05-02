#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class LocationUpdateRequestDto
    {
        public int GarageId { get; set; }
        public string GarageAddress { get; set; }
        public string GarageWard { get; set; }
        public string GarageDistrict { get; set; }
        public string GarageCity { get; set; }
    }
}