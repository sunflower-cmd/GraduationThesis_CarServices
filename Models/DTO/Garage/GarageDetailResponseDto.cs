#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class GarageDetailResponseDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageImage { get; set; }
        public string GarageContactInformation { get; set; }
        public string GarageAbout { get; set; }
        public string GarageAddress { get; set; }
        public string GarageWard { get; set; }
        public string GarageDistrict { get; set; }
        public string GarageCity { get; set; }
        public string FromTo { get; set; }
        public string OpenAt { get; set; }
        public string CloseAt { get; set; }
        public int GarageStatus { get; set; }

        public byte[] VersionNumber { get; set; }

        public UserGarageDto UserGarageDto { get; set; }
        public ICollection<ReviewGarageDto> ReviewGarageDtos { get; set; }
        public ICollection<CouponGarageDto> CouponGarageDtos { get; set; }
        public ICollection<ServiceGarageGarageDto> ServiceGarageGarageDtos { get; set; }
    }
}