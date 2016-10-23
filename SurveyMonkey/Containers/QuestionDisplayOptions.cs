using System.Collections.Generic;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
	[JsonConverter(typeof(TolerantJsonConverter))]
	public class QuestionDisplayOptions
	{
		public string MiddleLabel { get; set; }
		public bool ShowDisplayNumber { get; set; }
		public string DisplaySubType { get; set; }
		public string RightLabel { get; set; }
		public string DisplayType { get; set; }
		public Dictionary<string, string> CustomOptions { get; set; }
		public string LeftLabel { get; set; }
	}
}
