#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class CreateReviewDto
    {
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}