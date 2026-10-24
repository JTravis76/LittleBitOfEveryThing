namespace WebAppFunction.Api.Entities;

public class DynamicConfigurations : AbstractEntity<long>
{
  public string Name { get; set; }
  public string ConfigValue { get; set; }
  public string DisplayName { get; set; }
}