#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceGarage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceGaragesId { get; set; }
        [Range(0, 100, ErrorMessage = "")]
        public int LotNumber { get; set; }
        [Column(TypeName = "tinyint")]
        public LotStatus LotStatus { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}