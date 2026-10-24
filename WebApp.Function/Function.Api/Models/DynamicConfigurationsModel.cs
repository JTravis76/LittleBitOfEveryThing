namespace WebAppFunction.Api.Models;

public class DynamicConfigurationsModel
{
  public long Id { get; set; }
  public string Name { get; set; }
  public string ConfigValue { get; set; }
  public string DisplayName { get; set; }
}