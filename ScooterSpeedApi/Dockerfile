FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ScooterSpeedApi/ScooterSpeedApi.csproj", "ScooterSpeedApi/"]
COPY ["ScooterSpeedApi.Data/ScooterSpeedApi.Data.csproj", "ScooterSpeedApi.Data/"]
COPY ["ScooterSpeedApi.Domain/ScooterSpeedApi.Domain.csproj", "ScooterSpeedApi.Domain/"]
COPY ["ScooterSpeedApi.Service/ScooterSpeedApi.Service.csproj", "ScooterSpeedApi.Service/"]
COPY ["ScooterSpeedApi.Messaging.Receive/ScooterSpeedApi.Messaging.Receive.csproj", "ScooterSpeedApi.Messaging.Receive/"]
COPY ["Tests/ScooterSpeedApi.Test/ScooterSpeedApi.Test.csproj", "Tests/ScooterSpeedApi.Test/"]  
COPY ["Tests/ScooterSpeedApi.Service.Test/ScooterSpeedApi.Service.Test.csproj", "Tests/ScooterSpeedApi.Service.Test/"]  
COPY ["Tests/ScooterSpeedApi.Data.Test/ScooterSpeedApi.Data.Test.csproj", "Tests/ScooterSpeedApi.Data.Test/"] 
COPY ["ScooterSpeedApi/nuget.config", ""]
COPY ["*.props", "./"]

ARG PAT=localhost
RUN sed -i "s|</configuration>|<packageSourceCredentials><MicroserviceDemoNugets><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></MicroserviceDemoNugets></packageSourceCredentials></configuration>|" nuget.config

RUN dotnet restore "ScooterSpeedApi/ScooterSpeedApi.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterSpeedApi.Test/ScooterSpeedApi.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterSpeedApi.Service.Test/ScooterSpeedApi.Service.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterSpeedApi.Data.Test/ScooterSpeedApi.Data.Test.csproj" --configfile "./nuget.config"
COPY . .

RUN dotnet build "ScooterSpeedApi/ScooterSpeedApi.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "Tests/ScooterSpeedApi.Test/ScooterSpeedApi.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/ScooterSpeedApi.Service.Test/ScooterSpeedApi.Service.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/ScooterSpeedApi.Data.Test/ScooterSpeedApi.Data.Test.csproj" -c Release --no-restore

FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId} 
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/ScooterSpeedApi.Test/ScooterSpeedApi.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/ScooterSpeedApi.Service.Test/ScooterSpeedApi.Service.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json Tests/ScooterSpeedApi.Data.Test/ScooterSpeedApi.Data.Test.csproj

FROM build AS publish
RUN dotnet publish "ScooterSpeedApi/ScooterSpeedApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScooterSpeedApi.dll"]