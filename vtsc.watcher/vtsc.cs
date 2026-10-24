using System.Collections.Generic;

namespace vtsc
{
    class Program
    {
        private static string rootDir = System.Environment.CurrentDirectory; //@"C:\Users\KDJ\source\repos\Project1\Vue\Templates"
        private static string outDir = System.Environment.CurrentDirectory;
        private static string searchPattern = "*.vue";
        private static bool includeSubDirectories = false;

        static void Main(string[] args)
        {
            //scan current directory for vtscconfig.json
            if (System.IO.File.Exists(rootDir + @"\vtscconfig.json"))
            {
                try
                {
                    string json = System.IO.File.ReadAllText(rootDir + @"\vtscconfig.json");
                    VtscConfigModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<VtscConfigModel>(json);

                    rootDir = model.RootDir;
                    outDir = model.OutDir;
                    includeSubDirectories = model.IncludeSubDirectories;
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine(e.Message);
                }
            }



            //List to store active file information
            Dictionary<string, string> activeDictionary = new Dictionary<string, string>();
            
            //Read all files from Directory based on search pattern
            string[] files = System.IO.Directory.GetFiles(rootDir, searchPattern, includeSubDirectories ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);

            //Add file information to active list
            foreach (string file in files)
            {
                //System.Console.WriteLine(file);
                //System.Console.WriteLine("");

                System.IO.FileInfo f = new System.IO.FileInfo(file);

                activeDictionary.Add(file, f.LastWriteTime.ToString());
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("  VTSC  Watching......");
            System.Console.WriteLine("");

            do
            {
                //TODO: may need to run this in different thread
                //checks if user wants to exit application
                //System.ConsoleKeyInfo keys = System.Console.ReadKey();
                //if (keys.Key == System.ConsoleKey.Q || (keys.Key == System.ConsoleKey.C && keys.Modifiers == System.ConsoleModifiers.Shift) )
                //{
                //    System.Console.WriteLine("");
                //    System.Console.Write("What to termate batch job (y/n)");

                //    var y = System.Console.ReadKey();
                //    if (y.Key == System.ConsoleKey.Y)
                //        break;
                //    else
                //    {
                //        System.Console.Clear();
                //        System.Console.WriteLine("");
                //        System.Console.WriteLine("  VTSC  Watching......");
                //        System.Console.WriteLine("");
                //    }
                //}

                System.Threading.Thread.Sleep(2000);

                //List to store newly scanned file information
                Dictionary<string, string> newDictionary = new Dictionary<string, string>();

                files = System.IO.Directory.GetFiles(rootDir, searchPattern, includeSubDirectories ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);

                //Add file information to new list
                foreach (string file in files)
                {
                    System.IO.FileInfo f = new System.IO.FileInfo(file);

                    newDictionary.Add(file, f.LastWriteTime.ToString());
                }

                // Scan for changes between active and new list
                foreach (var pair1 in activeDictionary)
                {
                    foreach (var pair2 in newDictionary)
                    {
                        if (pair1.Key == pair2.Key)
                        {
                            if (pair1.Value != pair2.Value)
                            {
                                //changes found: run update and copy new list to active list
                                UpdateFile(pair2.Key);
                                activeDictionary = newDictionary;
                            }
                        }
                    }
                }
            } while (true);
        }


        static void UpdateFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                System.Console.WriteLine("ERROR: Must have a full file path.");
                return;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder("`");
            bool isProcessing = false;

            if (System.IO.File.Exists(filePath))
            {
                System.Console.WriteLine($"{System.DateTime.Now}: File changed detected...");

                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (isProcessing)
                            sb.Append(line.Trim());

                        if (line.ToLower().Contains("<template"))
                            isProcessing = true;

                        if (line.ToLower().Contains("</template"))
                            isProcessing = false;
                    }
                }

                // parse out file name and change the ext from vue to ts
                string[] d = filePath.Split('\\');
                string fileName = d[d.Length - 1];
                fileName = fileName.Replace(".vue", ".ts");

                if (System.IO.File.Exists(outDir + "\\" + fileName))
                {
                    string text = System.IO.File.ReadAllText(outDir + "\\" + fileName);

                    int start = text.IndexOf("`") + 1;
                    int cnt = text.LastIndexOf("`") - start;

                    text = text.Remove(start, cnt);

                    text = text.Replace("``", sb.Append("`").ToString());

                    System.IO.File.WriteAllText(outDir + "\\" + fileName, text);

                    System.Console.WriteLine($"{System.DateTime.Now}: File change completed. Watching...");
                }
            }
        }
    }


    public class VtscConfigModel
    {
        public string RootDir { get; set; }
        public string OutDir { get; set; }
        public bool IncludeSubDirectories { get; set; }
    }
}
