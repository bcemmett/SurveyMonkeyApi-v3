using System.Collections.Generic;
using SurveyMonkey.Containers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SurveyMonkey
{
    public class SurveyMonkeyApi
    {
        private string _apiKey;
        private string _oAuthSecret;
        private IWebClient _webClient;

        public SurveyMonkeyApi(string apiKey, string oAuthSecret)
        {
            _apiKey = apiKey;
            _oAuthSecret = oAuthSecret;
            _webClient = new LiveWebClient();
            _webClient.Encoding = Encoding.UTF8;
        }

        internal SurveyMonkeyApi(string apiKey, string oAuthSecret, IWebClient webClient)
        {
            _apiKey = apiKey;
            _oAuthSecret = oAuthSecret;
            _webClient = webClient;
        }

        public List<Survey> GetSurveys()
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var surveys = result.ToObject<List<Survey>>();
            return surveys;
        }

        private JToken MakeApiRequest(string endpoint, Verb verb, RequestData data)
        {
            ResetWebClient();
            string result;

            _webClient.Headers.Add("Content-Type", "application/json");
            _webClient.Headers.Add("Authorization", "bearer " + _oAuthSecret);
            _webClient.QueryString.Add("api_key", _apiKey);
            if (verb == Verb.GET)
            {
                foreach (var item in data)
                {
                    _webClient.QueryString.Add(item.Key, item.Value.ToString());
                }
                result = _webClient.DownloadString(endpoint);
            }
            else
            {
                var settings = JsonConvert.SerializeObject(data);
                result = _webClient.UploadString(endpoint, verb.ToString(), settings);
            }
                
            var parsed = JObject.Parse(result);
            return parsed["data"];
        }

        private void ResetWebClient()
        {
            _webClient.Headers.Clear();
            _webClient.QueryString.Clear();
        }
    }

    internal class RequestData : Dictionary<string, object>
    {
    }
}