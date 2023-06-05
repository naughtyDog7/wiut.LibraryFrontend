@echo off

nssm remove LibraryWebApp confirm
nssm install LibraryWebApp "C:\inetpub\wwwroot\LibraryWebApp\published\LibraryWebApp.exe"
nssm set LibraryWebApp Start SERVICE_AUTO_START
nssm set LibraryWebApp AppStdout "C:\inetpub\wwwroot\LibraryWebApp\logs\stdout.log"
nssm set LibraryWebApp AppStderr "C:\inetpub\wwwroot\LibraryWebApp\logs\stderr.log"