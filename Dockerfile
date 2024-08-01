FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /MVC

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /MVC
COPY --from=build-env /MVC/out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "MVC.dll"]