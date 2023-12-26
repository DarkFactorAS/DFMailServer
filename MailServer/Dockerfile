# Use .Net Core 3.1 image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Flush all nuget repos
#RUN dotnet nuget locals all -c

# Copy files
COPY ./ ./

# Copy nuget file
COPY ./NuGet_Docker.config ./NuGet.config

# Restore and build web
RUN dotnet restore MailServer.csproj
RUN dotnet publish MailServer.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MailServer.dll"]

EXPOSE 5000:80
