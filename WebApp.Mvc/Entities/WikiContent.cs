using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Mvc.Entities
{
    [Table("WikiContent")]
    public class WikiContent
    {
        [Key]
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