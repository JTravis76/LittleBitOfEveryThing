namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPDF
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        byte[] Generate_A630(string location);
    }
}
