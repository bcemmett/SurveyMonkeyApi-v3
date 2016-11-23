#Examples
##Setup
####Installation
These examples assume you've added the library to your project with NuGet:
```
PM> Install-Package SurveyMonkeyApi
```
As a minimum, you should add the following declarations:
```csharp
using SurveyMonkey;
using SurveyMonkey.Containers;
```

####Authentication
SurveyMonkey provides two possible authentication mechanisms. For apps registered before 1st November 2016, an api key and access token must be provided (SurveyMonkey calls this OLD authentication). For apps created after 1st November 2016, only an access token is required, without an api key (SurveyMonkey calls this NEW authentication). The keys or tokens are passed into this library in the constructor of the SurveyMonkeyApi class.

For OLD apps created before 1st November 2016:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    //Do api access here
}
```

For NEW apps created after 1st November 2016:
```csharp
using (var api = new SurveyMonkeyApi("accessToken"))
{
    //Do api access here
}
```

For the remainder of these examples, we'll use the pattern for OLD apps created before 1st November 2016.

##Basic usage
####Surveys
To retrieve a list of Surveys, do:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    List<Survey> surveys = api.GetSurveyList();
}
```
The `Id` and `Title` properties will be populated - other properties will be `null`.

To retrieve details of a Survey's structure given its Id (eg 12345), do:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    Survey survey = api.GetSurveyDetails(12345);
}
```

The Survey object's complete structure is now populated, including Pages and Questions.

####Responses
To retrieve a list of Responses for surveyId 12345:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    List<Response> responses = api.GetSurveyResponseDetailsList(12345);
}
```

Or for collectorId 54321:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    List<Response> responses = api.GetCollectorResponseDetailsList(54321);
}
```

You can also see just an individual responseId 6789 for surveyId 12345:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    Response response = api.GetSurveyResponseDetails(12345, 6789);
}
```

For any of these methods, you can replace `Details` with `Overview` to return less data, eg `GetSurveyResponseOverviewList(surveyId)`.

####Collectors
To retrieve a list of Collectors for surveyId 12345, do:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    List<Collector> collectors = api.GetCollectorList(12345);
}
```

As with retrieving Surveys, only the `Id` and `Title` properties will be populated.

To retrieve the Collector's details given its collectorId (eg 54321), do:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    Collector collector = api.GetCollectorDetails(54321);
}
```

####Other
The are many other object types which can be retrieved with similarly-named methods, including Groups, Members, Survey Categories, Survey Templates, Users, Pages, and Questions.

##Configuring requests
Many api requests can be configured by providing additional settings. In these cases, the method will take an optional Settings object, the type of which is dependent on the endpoint. For example, when retrieving a list of responses, to look only at responses for the past 7 days where the ip address was 123.123.123.123, do:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    var settings = new GetResponseListSettings()
    {
        Ip = "123.123.123.123",
        StartModifiedAt = DateTime.UtcNow.AddDays(-7)
    };
    List<Response> responses = api.GetSurveyResponseDetailList(12345, settings);
}
```
####Paging
When retrieving lists of objects, SurveyMonkey breaks the data up into pages with a maximum of either 100 or 1000 objects. By default when you call one of these methods, the library will automatically make multiple api requests for you to retrieve the entire dataset. All these methods take an optional settings object which inherits from `IPagingSettings`, so will have two nullable int properties - Page and PerPage. If you pass in a settings object that has either of these objects set, the library's automatic paging will be disabled, and your manual choice of pages will be retrieved. Eg:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    var settings = new GetSurveyListSettings()
    {
        Page = 3,
        PerPage = 50
    };
    List<Survey> surveys = api.GetSurveyList(settings);
}
```

##Question <-> Response mapping
For most question types, Response objects from the api don't contain the text of respondents' actual selections, but rather provide numeric references to the Rows / Columns / Choices / Other which form part of the Survey's separate structure. These values have to be looked up, which a different mapping approach for each question type.

For any given surveyId, you can do:

```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    Survey survey = api.PopulateSurveyResponseInformation(12345);
}
```

This automatically retrieves the Survey's structure and all responses, then performs the Question <-> Response mapping operation, exposing a new property on every ResponseQuestion called `ProcessedAnswer`. This includes a property `Response` which includes all information available by mapping the response against the Survey structure.

You can also perform this mapping for multiple Surveys at once. For example, to retrieve fully mapped Surveys and Responses for your entire account, do:

```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    List<Survey> surveys = api.GetSurveyList();
    List<long> surveyIds = surveys.Select(s => s.Id.Value).OrderBy(s => s).ToList();
    List<Survey> populatedSurveys = api.PopulateSurveyResponseInformationBulk(surveyIds);
}
```

##Other notes
Some of the following examples require passing additional values into `SurveyMonkeyApi`'s constructor. When using NEW authentication for apps created after 1st November 2016, there are equivalent constructors which simply omit the `apiKey` parameter.
####Rate limiting
SurveyMonkey has a default rate limit of 2 requests per second, so the library will wait a minimum of 500ms between making requests.

If you're entitled to make requests more frequently, you can pass a different delay length into the constructor. For example, to make 10 requests per second:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken", 100))
{
    //Do stuff
}
```

####Retry logic
By default, if there is a problem communicating with the api, the library will automatically retry after 5, 30, 300 and 900 seconds. To override this behaviour, pass an array of ints into the constructor representing the sequence of delays (in seconds) betewen each attempy. To disable retry behaviour entirely, pass in `null` or an empty array.
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken", new [] {10, 60, 300}))
{
    //Will wait 10 seconds, then 1 minute, then 5 minutes (4 attempts in total)
}
```

```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken", null))
{
    //No retry behaviour - a WebException will be thrown if there is any error reaching the api
}
```

You can combine a custom retry sequence with a custom rate limit using:
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken", 250, new [] {60, 120}))
{
    //Makes up to 4 requests per second, and retries after 1 then 2 minutes in case of failure
}
```

####Disposing
`SurveyMonkeyApi` implements IDisposable. You should wrap it in a `using` block (as per the examples on this page), or call `Dispose()` on it once you are finished with it.

####Dates
All dates returned from the library are in UTC. If you supply a date as part of a request filter, `DateTime` objects which have a `Local` DateTimeKind will be converted into UTC by the library, while any with the `Unspecified` kind will be treated as if they were UTC.

##Worked examples
####Polling
A common scenario is to check the api periodically for any new data. In this example we check every hour for any new Responses to a Survey. Note that in this particular example, it might be preferable to use [webhooks](https://developer.surveymonkey.com/api/v3/#webhooks) to provide the notification, but the general principle still applies.
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    //Get the id of our survey (or store this locally to avoid the api call)
    List<Survey> surveys = api.GetSurveyList();
    long surveyId = surveys.First(s => s.Title == "Customer Feedback Survey").Id.Value;

    //Remember when we last did a check
    DateTime lastCheck = DateTime.UtcNow;

    while (true)
    {
        //Wait 1 hour
        Thread.Sleep(60 * 60 * 1000);

        //Create a settings object to get only responses added since our last check
        var settings = new GetResponseListSettings { StartCreatedAt = lastCheck };

        //Remember the current time for filtering the next run
        lastCheck = DateTime.UtcNow;

        //Get any new responses
        List<Response> newResponses = api.GetSurveyResponseDetailsList(surveyId, settings);

        //Do something with any new responses
        foreach (Response response in newResponses)
        {
            Console.WriteLine("New response from {0} -  view at {1}", response.IpAddress, response.AnalyzeUrl);
        }
                    
        //Go again
    }
}
```

####Response question mapping
Having retrieved response data, you typically want to map it against the survey's structure to understand its meaning, rather than displaying the integer ids of items a respondent selected. This example shows how that mapping works for two of the simpler question types: open-ended questions, and single-choice questions.
```csharp
using (var api = new SurveyMonkeyApi("apiKey", "accessToken"))
{
    long surveyId = 123;
    long openEndedQuestionId = 456;
    long singleChoiceQuestionId = 789;

    //Get the survey's structure
    Survey survey = api.GetSurveyDetails(surveyId);

    //Retrieve any responses
    List<Response> responses = api.GetSurveyResponseDetailList(surveyId);

    foreach (Response response in responses)
    {
        //Open ended questions are easy - a single Answer whose Text property we want
        string openEnded = response.Questions
            .FirstOrDefault(q => q.Id == openEndedQuestionId)
            ?.Answers
            .FirstOrDefault()
            ?.Text;

        //Single choice questions are more involved. Much of the null handling is omitted for clarity
        //First we find the response's question we're interested in
        ResponseQuestion singleChoiceQuestion = response.Questions
            .FirstOrDefault(q => q.Id == singleChoiceQuestionId);

        //Next we find the response's answer which is set (there should only be one in this case) 
        long? choiceId = singleChoiceQuestion?.Answers
            .FirstOrDefault(a => a.ChoiceId != null)
            ?.ChoiceId;

        //Now we lookup this choiceId in the survey's structure
        string singleChoice = survey?.Questions
            .FirstOrDefault(q => q.Id == singleChoiceQuestionId)
            .Answers.Choices.First(c => c.Id == choiceId).Text;
    }
```
For other question types, the mapping can become quite complex. It's often worth letting the library handle this mapping by using `PopulateSurveyResponseInformation()` which presents you with mapped objects (details above). Alternatively, the mapping logic is contained within [SurveyMonkeyApi.DataProcessing.cs](https://github.com/bcemmett/SurveyMonkeyApi-v3/blob/master/SurveyMonkey/SurveyMonkeyApi.DataProcessing.cs) and [QuestionAnswers.cs](https://github.com/bcemmett/SurveyMonkeyApi-v3/blob/master/SurveyMonkey/Containers/QuestionAnswers.cs) if you wish to emulate sections of it.
