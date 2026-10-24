using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Api.Entities
{

#nullable disable

    [Table("WikiContent", Schema = "dbo")]
    public class WikiContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? ModifiedDttm { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool IsHome { get; set; }

        public int? Hits { get; set; }

        public int? Version { get; set; }

        public bool IsPublish { get; set; }

        public string Tags { get; set; }
    }
}
