using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SurveyMonkey.Containers
{
    [JsonConverter(typeof(TolerantJsonConverter))]
    public class QuestionAnswers
    {
        public List<Choice> Choices { get; set; }
        public List<Row> Rows { get; set; }
        public List<Column> Cols { get; set; }
        public OtherAnswer Other { get; set; }

        internal Dictionary<long, string> ItemLookup { get; set; }
        internal Dictionary<long, string> ColChoicesLookup { get; set; }
        internal Dictionary<long, string> DemographicTypeLookup { get; set; }

        internal void PopulateItemLookup()
        {
            ItemLookup = new Dictionary<long, string>();
            if (Choices != null)
            {
                foreach (var item in Choices)
                {
                    AddItemToDictionary(item.Id, item.Text);
                }
            }
            if (Rows != null)
            {
                foreach (var item in Rows)
                {
                    AddItemToDictionary(item.Id, item.Text);
                }
            }
            if (Cols != null)
            {
                foreach (var col in Cols)
                {
                    AddItemToDictionary(col.Id, col.Text);
                }
            }
            if (Other != null)
            {
                AddItemToDictionary(Other.Id, Other.Text);
            }
        }

        internal void PopulateDemographicTypeLookup()
        {
            if (Rows != null)
            {
                DemographicTypeLookup = Rows.Where(r => r.Id.HasValue).ToDictionary(r => r.Id.Value, r => r.Type);
            }
            
        }

        internal void PopulateColChoicesLookup()
        {
            if (Cols != null)
            {
                ColChoicesLookup = Cols
                    .Where(answerItem => answerItem.Choices != null)
                    .SelectMany(a => a.Choices)
                    .ToDictionary(item => item.Id.Value, item => item.Text);
            }
        }

        private void AddItemToDictionary(long? key, string value)
        {
            if (key.HasValue && !ItemLookup.ContainsKey(key.Value))
            {
                ItemLookup.Add(key.Value, value);
            }
        }
    }
}