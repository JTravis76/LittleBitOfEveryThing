using WebApp.Api.Entities;
using WebApp.Api.Models;

namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurationSvc
    {
        /// <summary>
        /// Fetch a collection of application Configurations
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Configuration>> GetConfigurationsAsync();

        /// <summary>
        /// Paginate through a collection of Configurations
        /// </summary>
        /// <param name="page">page number (default is 1)</param>
        /// <param name="pagesize">number of records to return (default is 25)</param>
        /// <param name="search">text to search</param>
        /// <returns></returns>
        public Task<ConfigurationPager> GetConfigurationPager(int page = 1, int pagesize = 25, string search = "");

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
        public Task<int> UpdateConfiguration(Configuration configuration);
    }
}
