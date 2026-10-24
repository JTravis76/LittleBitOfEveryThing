using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFunction.Api.Entities
{
  [Table("GrantorTypes", Schema = "dbo")]
  public class GrantorType
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Grantor Name is required")]
    [StringLength(150)]
    public string Name { get; set; }

    public bool Active { get; set; }
  }
}
