using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
    [Table("GroupMember", Schema = "dbo")]
    public class GroupMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Group Id is required")]
        [Column(Order = 0, TypeName = "INT")]
        public int GroupId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [Column(Order = 1, TypeName = "VARCHAR")]
        public string? UserName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? Email { get; set; }



        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.Input)]
        [ForeignKey("GroupId")]
        public virtual Group? Group { get; set; }
    }
}
