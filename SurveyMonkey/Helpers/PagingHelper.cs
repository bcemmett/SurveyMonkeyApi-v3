using System;
using System.Collections.Generic;
using System.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey.Helpers
{
    internal class PagingHelper
    {
        internal static IEnumerable<IPageable> Page(IPageableSettings settings, Func<RequestData, IEnumerable<IPageable>> requestMethod, int perPage)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return requestMethod(requestData);
            }

            //Auto-page
            const int maxResultsPerPage = 1000;
            var results = new List<IPageable>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = requestMethod(requestData);
                if (newResults.Any())
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count() < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }
    }
}