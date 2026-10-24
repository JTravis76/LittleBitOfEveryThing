//namespace WebApp.Api.Services.Documentum
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class DocumentumSvc:  Interfaces.IMedia
//    {
//        private readonly DocumentumClient _rest;
//        private readonly IConfiguration _configuration;
//        private readonly string _cabinet = string.Empty;

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="configuration"></param>
//        /// <exception cref="ArgumentNullException"></exception>
//        public DocumentumSvc(IConfiguration configuration)
//        {
//            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
//            _cabinet = _configuration["AppSettings:Documentum:Cabinet"];

//            _rest = new DocumentumClient(_configuration);
//        }

//        #region Private Methods

//        /// <summary>
//        /// Returns the URL of the media
//        /// </summary>
//        /// <param name="objectid">Properties["r_object_id"]</param>
//        /// <returns></returns>
//        private string GetMediaUri(string objectid)
//        {
//            Emc.Documentum.Rest.DataModel.Document doc = _rest.repository.GetSysObjectById<Emc.Documentum.Rest.DataModel.Document>(objectid);
//            Emc.Documentum.Rest.DataModel.ContentMeta content = doc.GetPrimaryContent(new Emc.Documentum.Rest.Net.SingleGetOptions());
//            if (content == null)
//                return null;

//            return content.GetMediaUri();
//        }

//        /// <summary>
//        /// No objectid entry link can strip id from uri such as folders/3987392847adf789
//        /// </summary>
//        /// <param name="uri"></param>
//        /// <returns></returns>
//        private string GetObjectIdFromUri(string uri)
//        {
//            string tmp = uri;
//            //if (tmp.Substring(tmp.Length - 8, tmp.Length).Equals("/folders")) tmp = tmp.Substring(0, tmp.Length - 8);
//            if (tmp.Substring(tmp.Length - 8, 8).Equals("/folders")) tmp = tmp.Substring(0, 8);

//            int idx = tmp.LastIndexOf("/");
//            //string id = tmp.Substring(idx + 1, tmp.Length);
//            string id = tmp.Substring(idx + 1, 16);
//            return id;

//        }

//        /// <summary>
//        /// Returns Cabinet object with that name
//        /// </summary>
//        private Emc.Documentum.Rest.DataModel.Cabinet GetCabinet(string name)
//        {
//            Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.Cabinet> cabinets = _rest.repository.GetCabinets<Emc.Documentum.Rest.DataModel.Cabinet>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Links = true });
//            Emc.Documentum.Rest.DataModel.Cabinet cab = cabinets.FindInlineEntry(name);
//            return cab;
//        }

//        /// <summary>
//        /// Pass in full path and returns Folder object at that path
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        private Emc.Documentum.Rest.DataModel.Folder GetFolderByFullPath(string path)
//        {
//            string[] crumbs = PathToBreadcrumbs(path);
//            string cab = crumbs[0];

//            Emc.Documentum.Rest.DataModel.Cabinet c = GetCabinet(cab);
//            if (c == null) return null;

//            string objectId = (string)c.Properties["r_object_id"];
//            if (crumbs.Length == 1) throw new Exception("This is just a cabinet, use GetCabinet");

//            int count = 1;
//            Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.Folder> folders = null;
//            Emc.Documentum.Rest.DataModel.Folder folder = null;

//            while (count < crumbs.Length)
//            {
//                //get subfolder
//                if (count == 1)
//                {
//                    //find the folder from the cabinet
//                    //folders = c.GetFolders<Emc.Documentum.Rest.DataModel.Folder>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Filter = "starts-with(object_name, '"+crumbs[count]+"')", View = ":all" });
//                    folders = c.GetFolders<Emc.Documentum.Rest.DataModel.Folder>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Filter = "object_name =  '" + crumbs[count] + "'", View = ":all" });
//                    folder = folders.FindInlineEntry(crumbs[count]); //if that's it set the folder
//                }
//                else
//                {
//                    //find folder in folder
//                    //folders=folder.GetFolders<Emc.Documentum.Rest.DataModel.Folder>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Filter = "starts-with(object_name, '" + crumbs[count] + "')", View = ":all" });
//                    folders = folder.GetFolders<Emc.Documentum.Rest.DataModel.Folder>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Filter = "object_name eq '" + crumbs[count] + "'", View = ":all" });
//                    folder = folders.FindInlineEntry(crumbs[count]);
//                }
//                count++;
//            }
//            return folder;
//        }

//        /// <summary>
//        /// Splits path into string array or breadcrumb trail
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        private String[] PathToBreadcrumbs(string path)
//        {
//            String[] crumbs = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
//            return crumbs;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="s"></param>
//        /// <returns></returns>
//        private bool EndsWithSlash(string s)
//        {
//            if (string.IsNullOrEmpty(s)) return false;
//            return s.Substring(s.Length - 1, 1).Equals("/");
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="r_object_id"></param>
//        /// <returns></returns>
//        private Emc.Documentum.Rest.DataModel.Document GetDocumentById(string r_object_id)
//        {
//            string uri = _rest.RestHomeUri + (EndsWithSlash(_rest.RestHomeUri) ? "" : "/") + "repositories/" + _rest.repository.Name + "/documents/" + r_object_id;
//            Emc.Documentum.Rest.DataModel.Document doc = _rest.client.Get<Emc.Documentum.Rest.DataModel.Document>(uri, null);

//            doc.SetClient(_rest.client);
//            return doc;
//        }


//        #endregion

//        /// <summary>
//        /// Returns the file blob
//        /// </summary>
//        /// <param name="objectid"></param>
//        /// <returns></returns>
//        public System.IO.Stream GetPrimaryContent(string objectid)
//        {
//            //Emc.Documentum.Rest.DataModel.Document document = GetDocumentById(objectid);

//            System.Net.WebRequest myWebRequest = System.Net.WebRequest.Create(GetMediaUri(objectid));
//            myWebRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_rest.AuthInfo)));
//            myWebRequest.Headers.Add("CACHE-CONTROL", "NO-CACHE");
//            myWebRequest.Headers.Add("PRAGMA", "NO-CACHE");
//            System.Net.WebResponse myWebResponse = myWebRequest.GetResponse();
//            System.IO.Stream ReceiveStream = myWebResponse.GetResponseStream();
//            return ReceiveStream;
//        }

//        /// <summary>
//        /// Returns the file blob
//        /// </summary>
//        /// <param name="objectid"></param>
//        /// <returns></returns>
//        public byte[] GetPrimaryContentAsByteArray(string objectid)
//        {
//            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
//            {
//                GetPrimaryContent(objectid).CopyTo(ms);
//                return ms.ToArray();
//            }
//        }

//        /// <summary>
//        /// Get folders from selected folder
//        /// </summary>
//        /// <param name="folderPath"></param>
//        /// <returns></returns>
//        public List<Emc.Documentum.Rest.DataModel.Folder> GetDirectory(string folderPath = "")
//        {
//            if (string.IsNullOrEmpty(folderPath))
//                folderPath = _cabinet;

//            Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> feed =
//                    _rest.ExecuteDQL($"SELECT * FROM dm_folder WHERE FOLDER('{folderPath}') ORDER BY object_name",
//                    new Emc.Documentum.Rest.Net.FeedGetOptions { });

//            List<Emc.Documentum.Rest.DataModel.Folder> folders = new List<Emc.Documentum.Rest.DataModel.Folder>();
//            foreach (var item in feed.Entries)
//            {
//                folders.Add(new Emc.Documentum.Rest.DataModel.Folder()
//                {
//                    Definition = item.Content.Definition,
//                    Name = item.Content.Name,
//                    Properties = item.Content.Properties,
//                    Type = item.Content.Type
//                });
//            }
//            return folders;
//        }

//        /// <summary>
//        /// Get files from selected folder
//        /// </summary>
//        /// <param name="folderPath"></param>
//        /// <returns></returns>
//        public List<Emc.Documentum.Rest.DataModel.Document> GetFiles(string folderPath = "")
//        {
//            if (string.IsNullOrEmpty(folderPath))
//                folderPath = _cabinet;

//            Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> feed =
//                    _rest.ExecuteDQL($"SELECT *, r_content_size FROM dm_document WHERE FOLDER('{folderPath}') ORDER BY object_name",
//                    new Emc.Documentum.Rest.Net.FeedGetOptions { });

//            List<Emc.Documentum.Rest.DataModel.Document> docs = new List<Emc.Documentum.Rest.DataModel.Document>();
//            foreach (var item in feed.Entries)
//            {
//                docs.Add(new Emc.Documentum.Rest.DataModel.Document()
//                {
//                    Definition = item.Content.Definition,
//                    Href = GetMediaUri(item.Content.Properties["r_object_id"].ToString()),
//                    Name = item.Content.Name,
//                    Properties = item.Content.Properties,
//                    Type = item.Content.Type
//                });
//            }
//            return docs;
//        }

//        /// <summary>
//        /// Remove file from Documentum by [r_object_id]
//        /// </summary>
//        /// <param name="deleteList"></param>
//        public void RemoveFiles(string[] deleteList)
//        {
//            foreach (string objectID in deleteList)
//            {
//                if (objectID != "")
//                {
//                    Emc.Documentum.Rest.DataModel.Document doc = GetDocumentById(objectID.ToString());
//                    doc.Delete(new Emc.Documentum.Rest.Net.GenericOptions { });
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="file"></param>
//        /// <param name="media"></param>
//        /// <param name="projectName"></param>
//        public void UploadFiles(Microsoft.AspNetCore.Http.IFormFile file, dynamic media, string projectName)
//        {
//            //Documentum core type : dm_dcoument
//            string name = media.name;
//            string title = media.title;
//            string description = media.description;

//            Emc.Documentum.Rest.DataModel.Folder pFolder = GetFolderByFullPath($"{_cabinet}/{projectName}");

//            if (pFolder == null)
//            {
//                Emc.Documentum.Rest.DataModel.Cabinet cabinet = GetCabinet(_cabinet.Replace("/", ""));

//                //Create new folder
//                var folder = new Emc.Documentum.Rest.DataModel.Folder();
//                folder.Properties["object_name"] = projectName;
//                folder.Properties["r_object_type"] = "dm_folder";
//                folder.Properties["title"] = projectName;
//                folder.Properties["subject"] = "directory for project: " + projectName;

//                pFolder = cabinet.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//            }

//            //2nd: Upload any documents
//            var doc = new Emc.Documentum.Rest.DataModel.Document();
//            doc.Properties["r_object_type"] = "dm_document";
//            doc.Properties["object_name"] = name;
//            doc.Properties["title"] = title;
//            doc.Properties["subject"] = description;

//            doc = pFolder.CreateSubDocument(doc, new Emc.Documentum.Rest.Net.GenericOptions { });

//            using (System.IO.Stream stream = file.OpenReadStream())
//            {
//                doc.CreateContent(stream, file.ContentType, new Emc.Documentum.Rest.Net.GenericOptions { });
//            }
//        }


//        ///// <summary>
//        ///// Return files from Documentum for Maint. Logs and Requests
//        ///// </summary>
//        ///// <param name="_folder">/FBP/App_Data/NDA/Logs</param>
//        ///// <param name="_type">log_no or mr_no</param>
//        ///// <param name="_id">Id of _type to search against</param>
//        ///// <returns></returns>
//        //public List<Emc.Documentum.Rest.DataModel.Document> GetFiles(string _folder, string _type, string _id)
//        //{
//        //    Emc.Documentum.Rest.DataModel.Folder f = _rest.repository.GetFolderByPath(_folder);
//        //    string emctype = "dm_document";

//        //    if (_type == "mr_no") emctype = "fbp_nda_mr";
//        //    else if (_type == "log_no") emctype = "fbp_nda_log";

//        //    Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> feed =
//        //          _rest.ExecuteDQL("SELECT *, r_content_size FROM " + emctype + " WHERE FOLDER('" + _folder + "') and " + _type + "=" + _id + " ORDER BY object_name",
//        //          new Emc.Documentum.Rest.Net.FeedGetOptions { });

//        //    List<Emc.Documentum.Rest.DataModel.Document> docs = new List<Emc.Documentum.Rest.DataModel.Document>();
//        //    Emc.Documentum.Rest.DataModel.Document newdoc;
//        //    foreach (var entry in feed.Entries)
//        //    {
//        //        newdoc = new Emc.Documentum.Rest.DataModel.Document();
//        //        foreach (var prop in entry.Content.Properties)
//        //        {
//        //            newdoc.Properties[prop.Key] = prop.Value;
//        //        }
//        //        newdoc.Href = GetMediaUri(newdoc.Properties["r_object_id"].ToString());
//        //        docs.Add(newdoc);
//        //    }
//        //    return docs.ToList();
//        //}

//        ///// <summary>
//        ///// Upload files. To attach file to Batch, must have BatchNumber and LetterNumber within fbp_wise_media
//        ///// </summary>
//        ///// <param name="file"></param>
//        ///// <param name="media">Object that matches the fbp_conda_media in Documentum</param>
//        ///// <param name="RequestNumber">WISE Request Number</param>
//        //public void UploadFiles(System.Web.HttpPostedFile file, dynamic media, string RequestNumber)
//        //{
//        //    //Documentum core type : dm_dcoument
//        //    string name = media.name;
//        //    string title = media.title;
//        //    string description = media.description;

//        //    //custom Documentum properties 
//        //    string contentType = media.contentType;
//        //    string letternumber = media.letternumber;
//        //    string batch_no = media.batch_no;
//        //    string is_item_datafile = media.is_item_datafile;
//        //    string requestnumber = media.requestnumber;
//        //    string uploader = media.uploader;

//        //    Emc.Documentum.Rest.DataModel.Folder pFolder = GetFolderByFullPath("CONDA/" + RequestNumber);

//        //    if (pFolder == null)
//        //    {
//        //        Emc.Documentum.Rest.DataModel.Cabinet cabinet = GetCabinet("CONDA");

//        //        var folder = new Emc.Documentum.Rest.DataModel.Folder();
//        //        folder.Properties["object_name"] = RequestNumber;
//        //        folder.Properties["r_object_type"] = "dm_folder";
//        //        folder.Properties["title"] = RequestNumber;
//        //        folder.Properties["subject"] = "directory for CONDA " + RequestNumber;

//        //        pFolder = cabinet.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//        //    }

//        //    //2nd: Upload any documents
//        //    var doc = new Emc.Documentum.Rest.DataModel.Document();
//        //    doc.Properties["r_object_type"] = contentType ?? "dm_document";
//        //    doc.Properties["object_name"] = name;
//        //    doc.Properties["title"] = title;
//        //    doc.Properties["subject"] = description;

//        //    //Custom content type tags
//        //    if (contentType == "fbp_conda_media") {
//        //        doc.Properties["letternumber"] = letternumber;
//        //        doc.Properties["batch_no"] = batch_no;
//        //        doc.Properties["is_item_datafile"] = Convert.ToBoolean(is_item_datafile);
//        //        doc.Properties["requestnumber"] = requestnumber;
//        //        doc.Properties["uploader"] = uploader;
//        //    }

//        //    doc = pFolder.CreateSubDocument(doc, new Emc.Documentum.Rest.Net.GenericOptions { });

//        //    string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);

//        //    doc.CreateContent(file.InputStream, mimeType, new Emc.Documentum.Rest.Net.GenericOptions { });
//        //}

//        ///// <summary>
//        ///// Upload file from client to Documentum folder path "FBP/App_Data/NDA/". Mainly CONDA Maintenance Logs & Requests
//        ///// </summary>
//        ///// <param name="file"></param>
//        ///// <param name="parentFolder">use Logs or MR</param>
//        ///// <param name="media"></param>
//        //public void UploadFiles(System.Web.HttpPostedFile file, string parentFolder, dynamic media)
//        //{
//        //    string name = media.name;
//        //    string title = media.title;
//        //    string description = media.description;

//        //    //custom Documentum properties
//        //    string contentType = media.contentType;
//        //    string log_no = media.log_no;
//        //    string mr_no = media.mr_no;
//        //    string nt_creator = media.nt_creator;

//        //    //1st: Test if Folder exist. If not, create it
//        //    Emc.Documentum.Rest.DataModel.Folder pFolder = GetFolderByFullPath("FBP/App_Data/NDA/" + parentFolder);

//        //    if (pFolder == null)
//        //    {
//        //        Emc.Documentum.Rest.DataModel.Folder NdaFolder = GetFolderByFullPath("FBP/App_Data/NDA");

//        //        var folder = new Emc.Documentum.Rest.DataModel.Folder();
//        //        folder.Properties["object_name"] = parentFolder;
//        //        folder.Properties["r_object_type"] = "dm_folder";
//        //        folder.Properties["title"] = parentFolder;
//        //        folder.Properties["subject"] = "directory for Maint. " + parentFolder;

//        //        pFolder = NdaFolder.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//        //    }

//        //    //2nd: Upload any documents
//        //    var doc = new Emc.Documentum.Rest.DataModel.Document();
//        //    doc.Properties["r_object_type"] = contentType == null ? "dm_document" : contentType;
//        //    doc.Properties["object_name"] = name;
//        //    doc.Properties["title"] = title;
//        //    doc.Properties["subject"] = description;

//        //    //Custom content type tags
//        //    if (contentType == "fbp_nda_log") doc.Properties["log_no"] = log_no;
//        //    doc.Properties["mr_no"] = mr_no;
//        //    doc.Properties["nt_creator"] = nt_creator;

//        //    doc = pFolder.CreateSubDocument(doc, new Emc.Documentum.Rest.Net.GenericOptions { });

//        //    string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);

//        //    doc.CreateContent(file.InputStream, mimeType, new Emc.Documentum.Rest.Net.GenericOptions { });
//        //}

//        ///// <summary>
//        ///// Upload file created in-memory without extension to Documentum
//        ///// </summary>
//        ///// <param name="file"></param>
//        ///// <param name="mimeMapping">PDF = "application/pdf"</param>
//        ///// <param name="folderName"></param>
//        ///// <param name="mediaInfo"></param>
//        //public void UploadFiles(System.IO.Stream file, string folderName, dynamic mediaInfo, string mimeMapping = "application/pdf")
//        //{
//        //    if (mimeMapping == null || mimeMapping == "") throw new Exception("Must include a Mime Type!");

//        //    string name = mediaInfo.name;
//        //    string title = mediaInfo.title;
//        //    string description = mediaInfo.description;
//        //    string batchNo = mediaInfo.batch_no;
//        //    string letterNo = mediaInfo.letternumber;
//        //    string requestNo = mediaInfo.requestnumber;

//        //    //1st: Test if Folder exist. If not, create it
//        //    Emc.Documentum.Rest.DataModel.Folder pFolder = GetFolderByFullPath("CONDA/" + folderName);

//        //    if (pFolder == null)
//        //    {
//        //        //Since we are creating folders, first grab the cabinet
//        //        Emc.Documentum.Rest.DataModel.Cabinet CondaCabinet =  GetCabinet("CONDA");

//        //        var folder = new Emc.Documentum.Rest.DataModel.Folder();
//        //        folder.Properties["object_name"] = folderName;
//        //        folder.Properties["r_object_type"] = "dm_folder";
//        //        folder.Properties["title"] = folderName;
//        //        folder.Properties["subject"] = "";

//        //        pFolder = CondaCabinet.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//        //    }

//        //    //2nd: Upload any documents
//        //    var doc = new Emc.Documentum.Rest.DataModel.Document();
//        //    doc.Properties["r_object_type"] = "fbp_conda_media";
//        //    doc.Properties["object_name"] = name;
//        //    doc.Properties["title"] = title;
//        //    doc.Properties["subject"] = description;

//        //    doc.Properties["batch_no"] = batchNo;
//        //    doc.Properties["letternumber"] = letterNo;
//        //    doc.Properties["requestnumber"] = requestNo;

//        //    doc = pFolder.CreateSubDocument(doc, new Emc.Documentum.Rest.Net.GenericOptions { });

//        //    doc.CreateContent(file, mimeMapping, new Emc.Documentum.Rest.Net.GenericOptions { });
//        //}

//        ///// <summary>
//        ///// Upload file from client to Documentum folder path "FBP/App_Data/NDA/". Mainly CONDA Maintenance
//        ///// </summary>
//        ///// <param name="file"></param>
//        ///// <param name="parentFolder"></param>
//        ///// <param name="childFolder"></param>
//        ///// <param name="media"></param>
//        //public void UploadFiles(System.Web.HttpPostedFile file, string parentFolder, string childFolder, dynamic media)
//        //{
//        //    string name = media.name;
//        //    string title = media.title;
//        //    string description = media.description;

//        //    //1st: Test if Folder exist. If not, create it
//        //    Emc.Documentum.Rest.DataModel.Folder pFolder = GetFolderByFullPath("FBP/App_Data/NDA/" + parentFolder);

//        //    if (pFolder == null)
//        //    {
//        //        Emc.Documentum.Rest.DataModel.Folder NdaFolder = GetFolderByFullPath("FBP/App_Data/NDA");

//        //        var folder = new Emc.Documentum.Rest.DataModel.Folder();
//        //        folder.Properties["object_name"] = parentFolder;
//        //        folder.Properties["r_object_type"] = "dm_folder";
//        //        folder.Properties["title"] = parentFolder;
//        //        folder.Properties["subject"] = "directory for Maint. " + parentFolder;

//        //        pFolder = NdaFolder.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//        //    }


//        //    //2nd: Create a directory for the Station. Use Station name to help identify.
//        //    Emc.Documentum.Rest.DataModel.Folder cFolder = GetFolderByFullPath("FBP/App_Data/NDA/" + parentFolder + "/" + childFolder);
//        //    if (cFolder == null)
//        //    {
//        //        var folder = new Emc.Documentum.Rest.DataModel.Folder();
//        //        folder.Properties["object_name"] = childFolder;
//        //        folder.Properties["r_object_type"] = "dm_folder";
//        //        folder.Properties["title"] = childFolder;
//        //        folder.Properties["subject"] = childFolder;

//        //        cFolder = pFolder.CreateSubFolder(folder, new Emc.Documentum.Rest.Net.GenericOptions());
//        //    }


//        //    //3rd: Upload any documents
//        //    var doc = new Emc.Documentum.Rest.DataModel.Document();
//        //    doc.Properties["r_object_type"] = "dm_document";
//        //    doc.Properties["object_name"] = name;
//        //    doc.Properties["title"] = title;
//        //    doc.Properties["subject"] = description;

//        //    doc = cFolder.CreateSubDocument(doc, new Emc.Documentum.Rest.Net.GenericOptions { });

//        //    string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);

//        //    doc.CreateContent(file.InputStream, mimeType, new Emc.Documentum.Rest.Net.GenericOptions { });
//        //}


//    }
//}
