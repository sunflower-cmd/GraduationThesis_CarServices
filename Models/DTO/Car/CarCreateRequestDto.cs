#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Car
{
    public class CarCreateRequestDto
    {
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string CarLicensePlate { get; set; }
        public int CarYear { get; set; }
        public string CarBodyType { get; set; }
        public string CarFuelType { get; set; }
        public int NumberOfCarLot {get; set;}
        
        public int CustomerId { get; set; }
    }
}