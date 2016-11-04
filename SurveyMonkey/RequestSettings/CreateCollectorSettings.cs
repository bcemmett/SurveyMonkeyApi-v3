using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMonkey.RequestSettings
{
    public class CreateCollectorSettings
    {
        public enum TypeOption
        {
            weblink,
            email
        }

        public TypeOption type { get; set; }
        public string name { get; set; }

    }
}
