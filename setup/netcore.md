# [A walk in GraphQL](/README.md)

## .Net Core Setup

### Requirements

* Net Core 3.1.3
* DotNet Command (.NET Core CLI)

#### Create Project

* Execute: [source code path]>dotnet new webapi -n GraphQLNetCore --auth none

#### Add Dependencies

* Execute: [csproj's path created above]>
    dotnet add package GraphQL -v 2.4.0
    dotnet add package Microsoft.EntityFrameworkCore -v 3.1.3
    dotnet add package Microsoft.EntityFrameworkCore.InMemory -v 3.1.3
    dotnet add package GraphQL.Server.Ui.Playground -v 3.4.0

#### Add Run Profile by edit [.\Property\launchSettings.json] file

* Open IDE (VS 2019 ir VS Code) to add new item to collection profile:
    "Playground": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "ui/playground",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost:59370/"
    }

#### Add Run Profile by edit [.\appSettings.json] file

* Open IDE (VS 2019 ir VS Code) to add new entry:
  "GraphQLPath": "/graphql"

## Run Application

### Using Visual Studio (2019/17)

The VS is an excellent for run the playground.

1. Go to .net core folder exercise.
2. Open the project (.csproj file)
3. F5.

### DotNet Command (.NET Core CLI)

1. Open CMD
2. Go to project folder
3. Execute Dotnet Run
4. from any browse type [http://localhost:59370/ui/playground] in order to test any query or mutation

## Learning Resources

* [GrapQL official documentation](https://graphql.org/learn/)
* [graphql-Net documentation](https://graphql-dotnet.github.io/docs/getting-started/introduction/)