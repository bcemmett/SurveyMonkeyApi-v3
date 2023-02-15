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
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var user = result.ToObject<User>();
            return user;
        }

        public List<Group> GetGroupList()
        {
            var settings = new PagingSettings();
            return GetGroupListPager(settings);
        }

        public List<Group> GetGroupList(PagingSettings settings)
        {
            return GetGroupListPager(settings);
        }

        private List<Group> GetGroupListPager(IPagingSettings settings)
        {
            string endPoint = "/groups";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Group>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Group) o);
        }

        public Group GetGroupDetails(long groupId)
        {
            string endPoint = String.Format("/groups/{0}", groupId);
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var user = result.ToObject<Group>();
            return user;
        }

        public List<Member> GetMemberList(long groupId)
        {
            var settings = new PagingSettings();
            return GetMemberListPager(groupId, settings);
        }

        public List<Member> GetMemberList(long groupId, PagingSettings settings)
        {
            return GetMemberListPager(groupId, settings);
        }

        private List<Member> GetMemberListPager(long groupId, IPagingSettings settings)
        {
            string endPoint = String.Format("/groups/{0}/members", groupId);
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Member>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Member)o);
        }
        
        public Member GetMemberDetails(long groupId, long memberId)
        {
            string endPoint = String.Format("/groups/{0}", groupId);
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var member = result.ToObject<Member>();
            return member;
        }
    }
}