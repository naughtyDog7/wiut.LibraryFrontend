@echo off

nssm remove LibraryWebApp confirm
nssm install LibraryWebApp "C:\inetpub\wwwroot\LibraryWebApp\published\LibraryWebApp.exe"
nssm set LibraryWebApp Start SERVICE_AUTO_START
nssm start LibraryWebApp