#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [MaxLength(10)]
        public string RoleName { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<User> Users { get; set; }
    }
}