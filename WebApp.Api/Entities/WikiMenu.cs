using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Entities
{
#nullable disable

    [Table("WikiMenu", Schema = "dbo")]
    public class WikiMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Text is required.")]
        public string Text { get; set; }

        [Required]
        public int ContentId { get; set; }

        public int Ordering { get; set; }
    }
}
