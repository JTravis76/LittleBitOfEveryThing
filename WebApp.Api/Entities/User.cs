using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        public User()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        [Key]
        [EntityGraphQL.Schema.GraphQLIgnore]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [StringLength(255)]
        [Column(TypeName = "VARCHAR(255)")]
        public string? UserName { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(150)]
        [Column(TypeName = "VARCHAR")]
        public string? UserPassword { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? LastName { get; set; }

        [EntityGraphQL.Authorization.GraphQLAuthorize("HR")]
        public bool IsActive { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore]
        [Display(Name = "LastLogon")]
        public DateTime? LastLogon { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore]
        [Column(TypeName = "VARCHAR")]
        [StringLength(8000)]
        public string? ResetToken { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Title { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(8000)]
        public string? Bio { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Badge { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? RegisteredDate { get; set; }



        public ICollection<UserRole> UserRoles { get; set; }
    }
}
