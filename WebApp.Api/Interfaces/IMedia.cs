namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMedia
    {
        ///// <summary>
        ///// Collection of Documentum Directories
        ///// </summary>
        ///// <param name="folderPath"></param>
        ///// <returns></returns>
        //List<Emc.Documentum.Rest.DataModel.Folder> GetDirectory(string folderPath = "");

        ///// <summary>
        ///// Get files from selected folder
        ///// </summary>
        ///// <param name="folderPath">Name of a folder to search in</param>
        ///// <returns></returns>
        //List<Emc.Documentum.Rest.DataModel.Document> GetFiles(string folderPath = "");

        /// <summary>
        /// Upload file to media repository
        /// </summary>
        /// <param name="file"></param>
        /// <param name="media">custom media tags</param>
        /// <param name="projectName">Project folder to upload file to</param>
        void UploadFiles(Microsoft.AspNetCore.Http.IFormFile file, dynamic media, string projectName);

        /// <summary>
        /// Remove file from Documentum by [r_object_id]
        /// </summary>
        /// <param name="deleteList"></param>
        void RemoveFiles(string[] deleteList);

        /// <summary>
        /// Returns the file blob
        /// </summary>
        /// <param name="objectid"></param>
        /// <returns></returns>
        System.IO.Stream GetPrimaryContent(string objectid);

        /// <summary>
        /// Returns the file blob
        /// </summary>
        /// <param name="objectid"></param>
        /// <returns></returns>
        byte[] GetPrimaryContentAsByteArray(string objectid);
    }
}
