@echo off
rm -rf C:\inetpub\wwwroot\LibraryWebApp
cd C:\inetpub\wwwroot\LibraryWebApp
dotnet publish -c Release -o ./published