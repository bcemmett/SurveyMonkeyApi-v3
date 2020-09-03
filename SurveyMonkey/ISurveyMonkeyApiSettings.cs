using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey
{
    public interface ISurveyMonkeyApiSettings
    {
        string ApiUrl { get; set; }
        string ApiKey { get; set; }
        string AccessToken { get; set; }
        int? RateLimitDelay { get; set; }
        int[] RetrySequence { get; set; }
        IWebClient WebClient { get; set; }
    }
}
