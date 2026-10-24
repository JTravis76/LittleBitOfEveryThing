using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppFunction.Api.Entities
{
  [Table("Document", Schema = "dbo")]
  public class Document
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "INT")]
    public int Id { get; set; }

    /// <summary>
    /// Id of circuit the media belong too
    /// </summary>
    [Column(TypeName = "INT")]
    public int CircuitId { get; set; }

    /// <summary>
    /// display name for the file
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// uploaded file name + ext
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Filename { get; set; }

    /// <summary>
    /// content type of the media
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string FileContentType { get; set; }

    /// <summary>
    /// raw binary of the media
    /// </summary>
    [Column(TypeName = "VARBINARY")]
    public byte[] FileData { get; set; }
  }
}
