using System.Threading.Tasks;
using WebAppFunction.Api.Models;

namespace WebAppFunction.Api.Services.Interfaces;

public interface IConfigurationService
{
  public Task<ConfigurationModel> GetConfigurationModelAsync();
  public Task<DynamicConfigurationsModel[]> UpdateDynamicConfigurationsAsync(DynamicConfigurationsModel[] configurationsToUpdate);
}