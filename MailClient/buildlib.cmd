@echo off
REM Clear
rd /q /s .\bin\debug

REM Flush Nuget repo
REM dotnet nuget locals all -c

REM Build and pack common lib
dotnet restore MailClient.csproj
dotnet build MailClient.csproj
dotnet pack MailClient.csproj -o bin/debug

REM Push packet
dotnet nuget push .\bin\debug\DarkFactor.MailClient.Lib.*.nupkg --api-key 1337 --source DarkFactor --skip-duplicate

read -p "Press any key to resume ..."