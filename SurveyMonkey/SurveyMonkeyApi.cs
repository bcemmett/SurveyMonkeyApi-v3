using System.Collections.Generic;
using SurveyMonkey.Containers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SurveyMonkey
{
    public class SurveyMonkeyApi
    {
        private string _apiKey;
        private string _oAuthSecret;

        public SurveyMonkeyApi(string apiKey, string oAuthSecret)
        {
            _apiKey = apiKey;
            _oAuthSecret = oAuthSecret;
        }

        public List<Survey> GetSurveys()
        {
            string endPoint = "https://api.surveymonkey.net/v3/surveys";
            string verb = "GET";
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var surveys = result.ToObject<List<Survey>>();
            return surveys;
        }

        private JToken MakeApiRequest(string endpoint, string verb, RequestData data)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Authorization", "bearer " + _oAuthSecret);
                client.QueryString.Add("api_key", _apiKey);
                foreach (var item in data)
                {
                    client.QueryString.Add(item.Key, item.Value.ToString());
                }
                string result = client.DownloadString(endpoint);
                var parsed = JObject.Parse(result);
                return parsed["data"];
            }
        }  
    }

    internal class RequestData : Dictionary<string, object>
    {
    }
}