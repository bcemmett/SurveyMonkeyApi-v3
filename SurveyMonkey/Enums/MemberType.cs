using Newtonsoft.Json;

namespace SurveyMonkey.Enums
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public enum MemberType
    {
        Regular,
        AccountOwner,
        Admin
    }
}