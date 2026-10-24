using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppFunction.Api.Entities;

namespace WebAppFunction.Api.Services.Interfaces;

public interface IAdministrationService
{
  public Task<IEnumerable<GrantorType>> GetGrantorTypeAsync();

  public Task<int> UpdateGrantorTypeAsync(GrantorType grantorType);
}