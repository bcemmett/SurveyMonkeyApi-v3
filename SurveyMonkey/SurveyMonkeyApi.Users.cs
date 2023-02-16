using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        //Individual user
        public User GetUserDetails()
        {
            string endPoint = "/users/me";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var user = result.ToObject<User>();
            return user;
        }

        public async Task<User> GetUserDetailsAsync()
        {
            string endPoint = "/users/me";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var user = result.ToObject<User>();
            return user;
        }

        //Groups list
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

        public async Task<List<Group>> GetGroupListAsync()
        {
            var settings = new PagingSettings();
            return await GetGroupListPagerAsync(settings);
        }

        public async Task<List<Group>> GetGroupListAsync(PagingSettings settings)
        {
            return await GetGroupListPagerAsync(settings);
        }

        private async Task<List<Group>> GetGroupListPagerAsync(IPagingSettings settings)
        {
            string endPoint = "/groups";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Group>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Group)o);
        }

        //Individual group
        public Group GetGroupDetails(long groupId)
        {
            string endPoint = $"/groups/{groupId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var user = result.ToObject<Group>();
            return user;
        }

        public async Task<Group> GetGroupDetailsAsync(long groupId)
        {
            string endPoint = $"/groups/{groupId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var user = result.ToObject<Group>();
            return user;
        }

        //Members list
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
            string endPoint = $"/groups/{groupId}/members";
            const int maxResultsPerPage = 1000;
            var results = Page(settings, endPoint, typeof(List<Member>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Member)o);
        }

        public async Task<List<Member>> GetMemberListAsync(long groupId)
        {
            var settings = new PagingSettings();
            return await GetMemberListPagerAsync(groupId, settings);
        }

        public async Task<List<Member>> GetMemberListAsync(long groupId, PagingSettings settings)
        {
            return await GetMemberListPagerAsync(groupId, settings);
        }

        private async Task<List<Member>> GetMemberListPagerAsync(long groupId, IPagingSettings settings)
        {
            string endPoint = $"/groups/{groupId}/members";
            const int maxResultsPerPage = 1000;
            var results = await PageAsync(settings, endPoint, typeof(List<Member>), maxResultsPerPage);
            return results.ToList().ConvertAll(o => (Member)o);
        }

        //Individual member
        public Member GetMemberDetails(long groupId, long memberId)
        {
            string endPoint = $"/groups/{groupId}";
            JToken result = MakeApiGetRequest(endPoint, new RequestData());
            var member = result.ToObject<Member>();
            return member;
        }

        public async Task<Member> GetMemberDetailsAsync(long groupId, long memberId)
        {
            string endPoint = $"/groups/{groupId}";
            JToken result = await MakeApiGetRequestAsync(endPoint, new RequestData());
            var member = result.ToObject<Member>();
            return member;
        }
    }
}