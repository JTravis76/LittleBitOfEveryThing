using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
    [EntityGraphQL.Schema.MutationArguments]
    [Table("Group", Schema = "dbo")]
    public class Group
    {
        public Group()
        {
            this.GroupMember = new HashSet<GroupMember>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "INT")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Group Name is required")]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? Name { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.Input)]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? CreatedBy { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.Input)]
        [Column(TypeName = "DATETIME")]
        public DateTime? CreatedDate { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.Input)]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string? UpdatedBy { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.Input)]
        [Column(TypeName = "DATETIME")]
        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "BIT")]
        public bool IsDeleted { get; set; }




        public virtual ICollection<GroupMember> GroupMember { get; set; }
    }
}
