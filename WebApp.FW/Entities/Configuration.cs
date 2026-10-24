using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.FW.Entities
{
    [Table("Configuration", Schema = "dbo")]
    public class Configuration
    {
        [Key]
        [Column(Order = 0, TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string ConfigName { get; set; }

        [Column(Order = 1, TypeName = "NVARCHAR(MAX)")]
        [StringLength(4000)]
        public string ConfigValue { get; set; }

        [Column(Order = 2, TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string ModifiedBy { get; set; }

        [Column(Order = 3, TypeName = "DATETIME")]
        public DateTime ModifiedDttm { get; set; }

        [Column(Order = 4, TypeName = "BIT")]
        public bool? ConfigStatus { get; set; } = null;

        [Column(Order = 5, TypeName = "VARCHAR(300)")]
        [StringLength(300)]
        public string Description { get; set; }

        [Column(Order = 6, TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string ConfigType { get; set; }

    }
}