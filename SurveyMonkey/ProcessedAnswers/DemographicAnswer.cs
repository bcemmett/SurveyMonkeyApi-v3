using System;
using System.Text;

namespace SurveyMonkey.ProcessedAnswers
{
    public class DemographicAnswer : IProcessedResponse
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Printable
        {
            get
            {
                var sb = new StringBuilder();
                if (Name != null)
                {
                    sb.Append($"{nameof(Name)}: {Name}{Environment.NewLine}");
                }
                if (Company != null)
                {
                    sb.Append($"{nameof(Company)}: {Company}{Environment.NewLine}");
                }
                if (Address != null)
                {
                    sb.Append($"{nameof(Address)}: {Address}{Environment.NewLine}");
                }
                if (Address2 != null)
                {
                    sb.Append($"{nameof(Address2)}: {Address2}{Environment.NewLine}");
                }
                if (City != null)
                {
                    sb.Append($"{nameof(City)}: {City}{Environment.NewLine}");
                }
                if (State != null)
                {
                    sb.Append($"{nameof(State)}: {State}{Environment.NewLine}");
                }
                if (Zip != null)
                {
                    sb.Append($"{nameof(Zip)}: {Zip}{Environment.NewLine}");
                }
                if (Country != null)
                {
                    sb.Append($"{nameof(Country)}: {Country}{Environment.NewLine}");
                }
                if (Email != null)
                {
                    sb.Append($"{nameof(Email)}: {Email}{Environment.NewLine}");
                }
                if (Phone != null)
                {
                    sb.Append($"{nameof(Phone)}: {Phone}{Environment.NewLine}");
                }
                if (sb.Length == 0)
                {
                    return null;
                }
                return sb.ToString().TrimEnd();
            }
        }
    }
}