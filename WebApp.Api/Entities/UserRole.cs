using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
    [Table("UserRole", Schema = "dbo")]
    public class UserRole
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Application Name is required")]
        [StringLength(255)]
        [Column(Order = 1, TypeName = "VARCHAR")]
        public string? ApplicationName { get; set; }

        [Key]
        [ForeignKey("Users")]
        [Column(Order = 2, TypeName = "VARCHAR")]
        [StringLength(255)]
        //[EntityGraphQL.Schema.GraphQLIgnore]
        public string? UserName { get; set; }

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role Name is required")]
        [StringLength(255)]
        [Column(Order = 3, TypeName = "VARCHAR")]
        //[EntityGraphQL.Authorization.GraphQLAuthorize("HR")]
        public string? RoleName { get; set; }



        [ForeignKey("UserName")]
        public virtual User? User { get; set; }
    }
}
