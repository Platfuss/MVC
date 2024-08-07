FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /MVC
COPY . .
RUN dotnet restore MVC/MVC.csproj
RUN dotnet publish MVC/MVC.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /MVC
COPY --from=build-env /MVC/out .
#ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MVC.dll"]