@echo off
rm -rf C:\inetpub\wwwroot\LibraryWebApp
cd C:\inetpub\wwwroot\LibraryWebApp
dotnet publish -c Release -o ./published

nssm remove LibraryWebApp confirm
nssm install LibraryWebApp "C:\inetpub\wwwroot\LibraryWebApp\published\LibraryWebApp.exe"
nssm set LibraryWebApp Start SERVICE_AUTO_START
nssm set LibraryWebApp AppStdout "C:\inetpub\wwwroot\LibraryWebApp\logs\stdout.log"
nssm set LibraryWebApp AppStderr "C:\inetpub\wwwroot\LibraryWebApp\logs\stderr.log"
nssm set LibraryWebApp AppEnvironmentExtra ASPNETCORE_URLS=http://*:80
nssm start LibraryWebApp