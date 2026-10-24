using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFunction.Api.Classes;
using WebAppFunction.Api.Database;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;

namespace WebAppFunction.Api.Services;

public class ConfigurationService : IConfigurationService
{
  private readonly ROWContext _rowContext;
  private readonly ILogger<ConfigurationService> _logger;
  private readonly IMapper _mapper;

  private static readonly string _instrumentationKey = EnvironmentVariables.InstrumentationKey.Get();

  public ConfigurationService(
      ROWContext rowContext,
      ILogger<ConfigurationService> logger,
      IMapper mapper)
  {
    _rowContext = rowContext;
    _logger = logger;
    _mapper = mapper;
  }

  private async Task<IEnumerable<DynamicConfigurationsModel>> GetAllConfigurationModelsAsync() =>
      _mapper.Map<IEnumerable<DynamicConfigurationsModel>>(await _rowContext.Configuration.ToListAsync());

  public async Task<ConfigurationModel> GetConfigurationModelAsync()
  {
    var configurationModel = new ConfigurationModel
    {
      InstrumentationKey = _instrumentationKey
    };

    try
    {
      var dynamicConfigurations = await GetAllConfigurationModelsAsync();
      configurationModel.DatabaseConfigurations = dynamicConfigurations;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while loading dynamic configurations");
    }

    return configurationModel;
  }

  public async Task<DynamicConfigurationsModel[]> UpdateDynamicConfigurationsAsync(DynamicConfigurationsModel[] configurationsToUpdate)
  {
    //Get database entries whose IDs match the configuration IDs sent by the user to update
    var currentConfigurations = _rowContext.Configuration
        .Where(config => configurationsToUpdate.Select(c => c.Id)
            .Contains(config.Id)).ToList();

    List<DynamicConfigurations> updatedConfigurations = new List<DynamicConfigurations>();

    foreach (var configuration in configurationsToUpdate)
    {
      var currentConfiguration = currentConfigurations
          .FirstOrDefault(c => c.Id == configuration.Id);

      if (currentConfiguration == null)
      {
        _logger.LogError($"Unable to update configuration {currentConfiguration.Id} - no data found in database");
        return null;
      }

      currentConfiguration.ConfigValue = configuration.ConfigValue;
      updatedConfigurations.Add(currentConfiguration);
    }

    await _rowContext.SaveChangesAsync();
    return _mapper.Map<DynamicConfigurationsModel[]>(updatedConfigurations);
  }

}