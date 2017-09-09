using System;
using System.Text;
using System.IO;
using System.Net;

static class ServerCommunication
{
    public static void UploadDataGet(string url)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Method = "GET";
        String test = String.Empty;
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            test = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
        }
    }

    public static string DownloadDataGet(string url)
    {
        WebClient wc = new WebClient();
        byte[] raw = wc.DownloadData(url);
        return Encoding.UTF8.GetString(raw);
    }
}