#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Page
{
    public class SearchByNameRequestDto
    {
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
        public string Search {get; set;}
    }
}