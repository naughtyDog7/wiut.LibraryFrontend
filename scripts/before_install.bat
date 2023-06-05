@echo off

dotnet --version || powershell -Command "& { iwr -outf install-dotnet.ps1 https://dot.net/v1/dotnet-install.ps1; ./install-dotnet.ps1 -Version 6.0 }"
cd C:\inetpub\wwwroot\LibraryWebApp
dotnet publish -c Release -o ./published
call scripts\create_service.bat
