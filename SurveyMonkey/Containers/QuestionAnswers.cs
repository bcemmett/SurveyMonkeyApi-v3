using System.Collections.Generic;
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

        internal void PopulatedItemLookup()
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
                foreach (var item in Cols)
                {
                    AddItemToDictionary(item.Id, item.Text);
                }
            }
            if (Other != null)
            {
                AddItemToDictionary(Other.Id, Other.Text);
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