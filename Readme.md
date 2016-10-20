# SurveyMonkey Api Library v3
<a href="https://ci.appveyor.com/project/BenEmmett/surveymonkeyapi-v3"><img src="https://ci.appveyor.com/api/projects/status/jqvdpoasuvqhs1xb/branch/master?svg=true" alt="Project Badge"></a>

A .NET library for querying SurveyMonkey's v3 api. It aims to entirely abstract away the api, so you only ever deal with strongly-typed .NET objects. It can handle things like rate-limiting and paging for you, and will also take care of the fiddly mapping between raw responses and the survey structure.

Install it with nuget:
```
PM>Install-Package SurveyMonkeyApi
```

* [Usage examples](https://github.com/bcemmett/SurveyMonkeyApi-v3/blob/master/Examples.md)
* [License](https://github.com/bcemmett/SurveyMonkeyApi-v3/blob/master/License.md)
* [Build server](https://ci.appveyor.com/project/BenEmmett/surveymonkeyapi-v3/history)
* [SurveyMonkey api documentation](https://developer.surveymonkey.com/api/v3/)
