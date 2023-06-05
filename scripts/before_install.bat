@echo off
cd C:\inetpub\wwwroot\LibraryWebApp
echo Directory contents:
dir
dotnet publish -c Release -o ./published
call scripts\create_service.bat
