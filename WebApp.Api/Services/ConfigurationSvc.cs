using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApp.Api.Database;
using WebApp.Api.Entities;
using WebApp.Api.Interfaces;
using WebApp.Api.Models;

namespace WebApp.Api.Services
{
    public class ConfigurationSvc : IConfigurationSvc
    {
        public readonly DataContext _dataContext;

        public ConfigurationSvc(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }


        /// <summary>
        /// Fetch a collection of application Configurations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Configuration>> GetConfigurationsAsync()
        {
            return await _dataContext.Configurations
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Paginate through a collection of Configurations
        /// </summary>
        /// <param name="page">page number (default is 1)</param>
        /// <param name="pagesize">number of records to return (default is 25)</param>
        /// <param name="search">text to search</param>
        /// <returns></returns>
        public async Task<ConfigurationPager> GetConfigurationPager(int page = 1, int pagesize = 25, string search = "")
        {
            // Filtering
            var predicate = LinqKit.PredicateBuilder.New<Configuration>();
            predicate.Start(x => x.ConfigName != null); // <- Default filter

            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                predicate.And(x => x.ConfigName!.ToLower().Contains(search));
            }

            // Pagination
            int cnt = _dataContext.Configurations.AsNoTracking().Where(predicate).Count();
            int pagecount = ((cnt + pagesize) / pagesize);
            int skipTo = (page * pagesize) - pagesize;

            // Query data
            var configurations = await _dataContext.Configurations
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(x => x.ConfigName)
                .Skip(skipTo)
                .Take(pagesize)
                .ToListAsync();

            return new Models.ConfigurationPager
            {
                Page = page,
                PageSize = pagesize,
                Total = pagecount,
                Data = configurations
            };
        }

        /// <summary>
        /// Updates a Configuration setting
        /// </summary>
        /// <param name="configuration"></param>
        /// <remarks>
        /// There is (3) types of configurations: String(NULL), Object, or Array.
        /// String - can be use either to set a string value for a setting or as a on/off feature of a setting.
        /// Object - use a key/pair value for a list. [{"Name": "", "Value": ""}]
        /// Array - a list of strings. ["String1", "String2"]
        /// </remarks>
        /// <returns></returns>
        public async Task<int> UpdateConfiguration(Configuration configuration)
        {
            if (configuration.ConfigType == "Array")
                configuration.ConfigStatus = null;

            configuration.ModifiedDttm = DateTime.Now;
            _dataContext.Configurations.Add(configuration);

            return await _dataContext.SaveChangesAsync();
        }
    }
}
