using System.Net;

namespace RentalRoomCrawler.Helpers;

public static class DownloadHelper
{
    /// <summary>
    /// 
    /// This class is used for download image from URI 
    /// 
    /// </summary>
    /// 
    public static string DownloadImageFromUri(string url, string fileName)
    {
        try
        {
            Uri? uriResult;

            var result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result) return "";

            using var client = new WebClient();
            if (uriResult != null) client.DownloadFile(uriResult, fileName);
            return fileName;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return "";
        }
    }
}