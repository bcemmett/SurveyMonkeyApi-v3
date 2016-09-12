using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace SurveyMonkey
{
    public interface IWebClient
    {
        WebHeaderCollection Headers { get; set; }
        NameValueCollection QueryString { get; set; }
        Encoding Encoding { get; set; }

        string DownloadString(string url);
        string UploadString(string url, string verb, string body);
    }
}