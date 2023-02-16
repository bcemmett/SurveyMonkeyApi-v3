using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey
{
    internal interface IWebClient
    {
        WebHeaderCollection Headers { get; set; }
        NameValueCollection QueryString { get; set; }
        Encoding Encoding { get; set; }
        IWebProxy Proxy { get; set; }

        string DownloadString(string url);
        Task<string> DownloadStringTaskAsync(string url);
        string UploadString(string url, string verb, string body);
        Task<string> UploadStringTaskAsync(string url, string verb, string body);
        void Dispose();
    }
}