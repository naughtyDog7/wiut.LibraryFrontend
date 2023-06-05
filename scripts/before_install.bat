@echo off
rmdir /S /Q C:\inetpub\wwwroot\LibraryWebApp
nssm remove LibraryWebApp confirm 2>nul || echo.