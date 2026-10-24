using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFunction.Api.Database;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Services.Interfaces;

namespace WebAppFunction.Api.Services
{
  public class AdministrationService : IAdministrationService
  {
    private readonly ROWContext _rowContext;
    private readonly ILogger<AdministrationService> _logger;
    private readonly IMapper _mapper;

    public AdministrationService(
    ROWContext rowContext,
    ILogger<AdministrationService> logger,
    IMapper mapper)
    {
      _rowContext = rowContext;
      _logger = logger;
      _mapper = mapper;
    }

    #region Grantor Type

    public async Task<IEnumerable<GrantorType>> GetGrantorTypeAsync()
    {
      return await _rowContext.GrantorTypes
          .AsNoTracking()
          .OrderBy(x => x.Name)
          .ToListAsync();
    }

    public async Task<int> UpdateGrantorTypeAsync(GrantorType grantorType)
    {
      if (grantorType != null)
      {
        if (grantorType.Id > 0)
          _rowContext.Update(grantorType);
        else
        {
          grantorType.Active = true;
          _rowContext.Add(grantorType);
        }

      }
      return await _rowContext.SaveChangesAsync();
    }

    #endregion
  }
}
