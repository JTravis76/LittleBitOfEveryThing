
public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SampleWSPut(System.Data.SqlTypes.SqlString weburl, out System.Data.SqlTypes.SqlString returnval)
    {
        string url = System.Convert.ToString(weburl);
        string feedData = string.Empty;

        try
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            System.IO.Stream stream = null;
            System.IO.StreamReader streamReader = null;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "PUT"; // you have to change to 
            //PUT/POST/GET/DELETE based on your scenerio…
            request.ContentLength = 0;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            streamReader = new System.IO.StreamReader(stream);
            feedData = streamReader.ReadToEnd();
            response.Close();
            stream.Dispose();
            streamReader.Dispose();
        }
        catch (System.Exception ex)
        {
            Microsoft.SqlServer.Server.SqlContext.Pipe.Send(ex.Message.ToString());
        }
        returnval = feedData;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SampleWSPost(System.Data.SqlTypes.SqlString weburl, out System.Data.SqlTypes.SqlString returnval)
    {
        string url = System.Convert.ToString(weburl);
        string feedData = string.Empty;

        try
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            System.IO.Stream stream = null;
            System.IO.StreamReader streamReader = null;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            response = (System.Net.HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            streamReader = new System.IO.StreamReader(stream);
            feedData = streamReader.ReadToEnd();
            response.Close();
            stream.Dispose();
            streamReader.Dispose();
        }
        catch (System.Exception ex)
        {
            Microsoft.SqlServer.Server.SqlContext.Pipe.Send(ex.Message.ToString());
        }
        returnval = feedData;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SampleWSGet(System.Data.SqlTypes.SqlString weburl, out System.Data.SqlTypes.SqlString returnval)
    {
        string url = System.Convert.ToString(weburl);
        string feedData = string.Empty;

        try
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            System.IO.Stream stream = null;
            System.IO.StreamReader streamReader = null;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "GET";
            response = (System.Net.HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            streamReader = new System.IO.StreamReader(stream);
            feedData = streamReader.ReadToEnd();
            response.Close();
            stream.Dispose();
            streamReader.Dispose();
        }
        catch (System.Exception ex)
        {
            Microsoft.SqlServer.Server.SqlContext.Pipe.Send(ex.Message.ToString());
        }
        returnval = feedData;
    }
};
