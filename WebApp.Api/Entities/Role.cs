using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Api.Entities
{
    [Table("Role", Schema = "dbo")]
    public class Role
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Application Name is required")]
        [StringLength(255)]
        [Column(Order = 0, TypeName = "VARCHAR")]
        public string? ApplicationName { get; set; }

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role Name is required")]
        [StringLength(255)]
        [Column(Order = 1, TypeName = "VARCHAR")]
        public string? RoleName { get; set; }

        [StringLength(255)]
        [Column(Order = 2, TypeName = "VARCHAR")]
        public string? RoleDescription { get; set; }

        [Column(Order = 3, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Column(Order = 4, TypeName = "DATETIME")]
        public DateTime? CreatedDate { get; set; }

        [Column(Order = 5, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Column(Order = 6, TypeName = "DATETIME")]
        public DateTime? ModifiedDate { get; set; }
    }
}
