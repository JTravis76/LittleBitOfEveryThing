using System.ComponentModel.DataAnnotations;

namespace WebAppFunction.Api.Entities
{
  public abstract class AbstractEntity<T>
  {
    [Key]
    public T Id { get; set; }
  }
}