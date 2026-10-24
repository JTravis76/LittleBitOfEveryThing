using System.Collections.Generic;

namespace WebAppFunction.Api.Models;

public class ConfigurationModel
{
  public string InstrumentationKey { get; set; }
  public IEnumerable<DynamicConfigurationsModel> DatabaseConfigurations { get; set; }
}