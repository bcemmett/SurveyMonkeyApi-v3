using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuizResults
    {
        public int? Incorrect { get; set; }
        public int? PartiallyCorrect { get; set;}
        public int? TotalQuestions { get; set; }
        public int? TotalScore { get; set; }
        public int? Score { get; set; }
        public int? Correct { get; set; }
    }
}
