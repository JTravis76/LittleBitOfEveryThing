//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace WebApp.Mvc.Controllers
//{
//    public class WikiController : Controller
//    {
//        public ActionResult Index(int? id, int edit = 0)
//        {
//            Entities.WikiContent wiki;
//            var wikiHome = new Models.WikiHome
//            {
//                WikiContent = new Entities.WikiContent(),
//                InEditMode = Convert.ToBoolean(edit), //TODO: Security Checks
//                IsContentEditor = true //TODO: security check
//            };

//            //check if need to load Home Page
//            if (id == null)
//            {
//                wiki = Services.EFRepositorySvc
//                    .Get<Entities.WikiContent>(x => x.IsHome)
//                    .FirstOrDefault();

//                //if no Home page is found, return basic model
//                if (wiki == null)
//                {
//                    wikiHome.WikiContent.Title = "";
//                    wikiHome.Markdown = "<div class='alert alert-info'>It's lonely here! Assign a Home page.</div>";
//                    return View(wikiHome);
//                }
//                else
//                {
//                    wikiHome.WikiContent = wiki;
//                    wikiHome.Markdown = CommonMark.CommonMarkConverter.Convert(wiki.Content);

//                    //update the Hits counter if not in Edit Mode
//                    if (edit == 0)
//                    {
//                        wiki.Hits += 1;
//                        Services.EFRepositorySvc.Modified(wiki);
//                    }
//                }
//            }
//            else if (id == 0)
//            {
//                //Load a new content to be saved
//                wikiHome.InEditMode = true;
//                wikiHome.WikiContent.Hits = 0;
//                wikiHome.WikiContent.Version = 0;
//            }
//            else
//            {
//                //Find the publish content article
//                wiki = Services.EFRepositorySvc
//                    .Get<Entities.WikiContent>(x => x.WikiContentId == id && x.IsPublish)
//                    .FirstOrDefault();

//                if (wiki != null)
//                {
//                    wikiHome.WikiContent = wiki;
//                    wikiHome.Markdown = CommonMark.CommonMarkConverter.Convert(wiki.Content);

//                    //update the Hits counter if not in Edit Mode
//                    if (edit == 0)
//                    {
//                        wiki.Hits += 1;
//                        Services.EFRepositorySvc.Modified(wiki);
//                    }
//                }
//                else
//                {
//                    //article not found or un-publish ??
//                    wikiHome.WikiContent.Title = "Missing Article!";
//                    v.Markdown = "<div class='alert alert-danger'>OOPS! Cannot find this article.</div>";
//                    wikiHome.IsContentEditor = false; //prevent user from trying to edit this
//                }
//            }
//            return View(wikiHome);
//        }

//        public ActionResult _Navigation()
//        {
//            var navigation = new Models.WikiNavigation()
//            {
//                MainMenu = Services.EFRepositorySvc
//                    .Get<Entities.WikiMenu>()
//                    .OrderBy(x => x.Parent)
//                    .ThenBy(x => x.Ordering)
//                    .ToList(),
//                IsContentEditor = true //TODO: security check
//            };
//            return PartialView(navigation);
//        }

//        [HttpPost]
//        public ActionResult SaveContent(Models.WikiHome wikiHome)
//        {
//            if (ModelState.IsValid)
//            {
//                if (wikiHome.WikiContent.IsHome)
//                    Services.EFRepositorySvc.WikiContent.ResetHomePage();


//                wikiHome.WikiContent.ModifiedDttm = DateTime.Now;
//                wikiHome.WikiContent.ModifiedBy = HttpContext.User.Identity.Name;
//                wikiHome.WikiContent.Version = wikiHome.WikiContent.Version == null ? 1 : (wikiHome.WikiContent.Version + 1);

//                if (wikiHome.WikiContent.Id == 0)
//                    Services.EFRepositorySvc.Add(wikiHome.WikiContent);
//                else
//                    Services.EFRepositorySvc.Modified(wikiHome.WikiContent);
//            }

//            //pass back Id to navigate too
//            return Json(new { success = true, id = wikiHome.WikiContent.Id });
//        }

//        /// <summary>
//        /// Gets a list of [ALL] articles to display in a table
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ActionResult Articles()
//        {
//            var wikiArticles = new Models.WikiArticles
//            {
//                WikiContents = Services.EFRepositorySvc
//                    .Get<Entities.WikiContent>()
//                    .OrderBy(x => x.Title)
//                    .ToList()
//            };

//            return View(wikiArticles);
//        }

//        public ActionResult Menu()
//        {
//            var wikiMenuView = new Models.WikiMenuView()
//            {
//                WikiContent = Services.EFRepositorySvc
//                    .Get<Entities.WikiContent>(x => x.IsPublish)
//                    .OrderBy(x => x.Title)
//                    .ToList(),
//                WikiMenu = Services.EFRepositorySvc
//                    .Get<Entities.WikiMenu>()
//                    .ToList()
//            };
//            return View(wikiMenuView);
//        }

//        /// <summary>
//        /// Search Result page
//        /// </summary>
//        /// <param name="s">search critia</param>
//        /// <returns></returns>
//        public ActionResult Search(string s)
//        {
//            if (s == null) s = "";

//            string[] words = s.Split(new[] { ' ' });

//            //setup Predicate with default search
//            LinqKit.ExpressionStarter<Entities.WikiContent> predicate = LinqKit.PredicateBuilder
//                .New<Entities.WikiContent>();

//            if (s != "")
//            {
//                foreach (string word in words)
//                    predicate = predicate.Or(x => x.Tags.Contains(word));
//            }


//            var wikiSearch = new Models.WikiSearch()
//            {
//                WikiContents = Services.EFRepositorySvc
//                    .Get<Entities.WikiContent>(predicate)
//                    .ToList()
//            };
//            return View(wikiSearch);
//        }


//        /// <summary>
//        /// Gets a list of publish articles for the Parent Picker
//        /// </summary>
//        /// <returns>JSON string</returns>
//        [HttpGet]
//        public string GetArticles()
//        {
//            var model = Services.EFRepositorySvc
//                .Get<Entities.WikiContent>(x => x.IsPublish)
//                .Select(x => new { Text = x.Title, Value = x.WikiContentId })
//                .ToList();

//            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
//        }


//        /// <summary>
//        /// Gets a list of articles directly within the same branch of the menu tree
//        /// </summary>
//        /// <param name="group">ID of article to build the parent list and to remove itself from list </param>
//        /// <returns></returns>
//        [HttpGet]
//        public string GetArticleOrdering(int group)
//        {
//            //Models.WikiMenu menu = Services.EFRepository.GetById<Models.WikiMenu>(group);

//            //if (menu != null)
//            //{
//            List<Entities.WikiMenu> list = Services.EFRepositorySvc
//                .Get<Entities.WikiMenu>(x => x.Parent == group)
//                .OrderBy(x => x.Ordering)
//                .ToList();

//            //list.RemoveAll(x => x.ContentId == group);

//            var model = list
//                .Select(x => new { Text = x.Text, Value = x.Ordering })
//                .ToList();

//            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
//            //}
//            //return "";            
//        }

//        public ActionResult Syntax()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Media()
//        {
//            var wikiMedia = new Models.WikiMedia();

//            if (System.IO.Directory.Exists(Server.MapPath("~/App_Data")))
//            {
//                wikiMedia.Files = System.IO.Directory.GetFiles(Server.MapPath("~/App_Data"));
//            }
//            return View(wikiMedia);
//        }

//        public ActionResult RemoveMedia(string name)
//        {
//            if (!string.IsNullOrEmpty(name))
//            {
//                if (System.IO.File.Exists(Server.MapPath("~/App_Data/" + name)))
//                    System.IO.File.Delete(Server.MapPath("~/App_Data/" + name));
//            }
//            return Json(new { success = true });
//        }

//        public ActionResult UploadFiles()
//        {
//            // Checking no of files injected in Request object  
//            if (Request.Files.Count > 0)
//            {
//                try
//                {
//                    //  Get all files from Request object  
//                    HttpFileCollectionBase files = Request.Files;
//                    for (int i = 0; i < files.Count; i++)
//                    {
//                        //string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";  
//                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

//                        HttpPostedFileBase file = files[i];
//                        string fname;

//                        // Checking for Internet Explorer  
//                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
//                        {
//                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
//                            fname = testfiles[testfiles.Length - 1];
//                        }
//                        else
//                        {
//                            fname = file.FileName;
//                        }

//                        // Get the complete folder path and store the file inside it.  
//                        fname = System.IO.Path.Combine(Server.MapPath("~/App_Data/"), fname);
//                        file.SaveAs(fname);
//                    }
//                    // Returns message that successfully uploaded  
//                    return Json(new { success = true });
//                }
//                catch (Exception ex)
//                {
//                    return Json(new { success = false, msg = "Error: " + ex.Message });
//                }
//            }
//            else
//            {
//                return Json("No files selected.");
//            }
//        }

//        [HttpPost]
//        public ActionResult ParseMarkdown(string content)
//        {
//            return Content(CommonMark.CommonMarkConverter.Convert(content));
//        }

//        [HttpPost]
//        public ActionResult SaveMenu(ICollection<Entities.WikiMenu> model)
//        {
//            if (model != null)
//            {
//                Services.Repository.WikiMenu.RemoveAllMenus();
//                Services.EFRepositorySvc.Add(model);
//            }
//            return Json(new { success = true });
//        }
//    }
//}