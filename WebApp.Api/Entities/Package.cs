using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string? Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(4000)]
        public string? Description { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string? Version { get; set; }
    }
}
