@echo off
cd C:\inetpub\wwwroot\LibraryWebApp
dotnet publish -c Release -o ./published
call scripts\create_service.bat
