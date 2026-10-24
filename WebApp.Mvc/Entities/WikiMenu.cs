using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Mvc.Entities
{
    [Table("WikiMenu")]
    public class WikiMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Parent { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Link text is required.")]
        public string Text { get; set; }

        [Required]
        public int ContentId { get; set; }

        public int Ordering { get; set; }
    }
}