using System.Collections.Generic;

namespace WebApp.FW.Interfaces
{
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="media"></param>
        ///// <param name="projectName"></param>
        //Emc.Documentum.Rest.DataModel.ContentMeta UploadFiles(System.Web.HttpPostedFile file, dynamic media, string projectName);

        ///// <summary>
        ///// Remove file from Documentum by [r_object_id]
        ///// </summary>
        ///// <param name="deleteList"></param>
        //void RemoveFiles(string[] deleteList);

        ///// <summary>
        ///// Returns URL of selected file object
        ///// </summary>
        ///// <param name="objectid">id of object</param>
        ///// <returns></returns>
        //string GetUri(string objectid);

        ///// <summary>
        ///// Returns the file blob
        ///// </summary>
        ///// <param name="objectid"></param>
        ///// <returns></returns>
        //System.IO.Stream GetPrimaryContent(string objectid);

        ///// <summary>
        ///// Returns the file blob
        ///// </summary>
        ///// <param name="objectid"></param>
        ///// <returns></returns>
        //byte[] GetPrimaryContentAsByteArray(string objectid);

        ///// <summary>
        ///// Return files from Documentum for Maint. Logs and Requests
        ///// </summary>
        ///// <param name="_folder">/FBP/App_Data/NDA/Logs</param>
        ///// <param name="_type">log_no or mr_no</param>
        ///// <param name="_id">Id of _type to search against</param>
        ///// <returns></returns>
        //List<Emc.Documentum.Rest.DataModel.Document> GetFiles(string _folder, string _type, string _id);

        ///// <summary>
        ///// Upload files. To attach file to Batch, must have BatchNumber and LetterNumber within fbp_conda_media
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="fbp_conda_media">Object that matches the fbp_conda_media in Documentum</param>
        ///// <param name="RequestNumber">CONDA Request Number</param>
        //void UploadFiles(System.Web.HttpPostedFile file, dynamic media, string RequestNumber);

        ///// <summary>
        ///// Upload file from client to Documentum folder path "FBP/App_Data/NDA/". Mainly CONDA Maintenance Logs & Requests
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="parentFolder">use Logs or MR</param>
        ///// <param name="media"></param>
        //void UploadFiles(System.Web.HttpPostedFile file, string parentFolder, dynamic media);

        ///// <summary>
        ///// Upload file created in-memory without extension to Documentum
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="mimeMapping">PDF = "application/pdf"</param>
        ///// <param name="folderName"></param>
        ///// <param name="mediaInfo"></param>
        //void UploadFiles(System.IO.Stream file, string folderName, dynamic mediaInfo, string mimeMapping = "application/pdf");

        ///// <summary>
        ///// Upload file from client to Documentum folder path "FBP/App_Data/NDA/". Mainly CONDA Maintenance
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="parentFolder"></param>
        ///// <param name="childFolder"></param>
        ///// <param name="media"></param>
        //void UploadFiles(System.Web.HttpPostedFile file, string parentFolder, string childFolder, dynamic media);
    }
}
