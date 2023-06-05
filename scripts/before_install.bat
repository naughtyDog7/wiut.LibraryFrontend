@echo off
cd C:\inetpub\wwwroot\LibraryWebApp
echo Directory contents:
dir /ad
pwd
dotnet publish -c Release -o ./published
call scripts\create_service.bat
