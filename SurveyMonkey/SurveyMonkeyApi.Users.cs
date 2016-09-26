using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public User GetUserDetails()
        {
            string endPoint = "/users/me";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var user = result.ToObject<User>();
            return user;
        }

        public List<Group> GetGroupList()
        {
            var settings = new GetCollectorListSettings();
            return GetGroupListPager(settings);
        }

        public List<Group> GetGroupList(PagingSettings settings)
        {
            return GetGroupListPager(settings);
        }

        private List<Group> GetGroupListPager(IPageableSettings settings)
        {
            string endPoint = "/groups";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Group>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Group) o);
        }

        public Group GetGroupDetails(long groupId)
        {
            string endPoint = String.Format("/groups/{0}", groupId);
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, new RequestData());
            var user = result.ToObject<Group>();
            return user;
        }
    }
}