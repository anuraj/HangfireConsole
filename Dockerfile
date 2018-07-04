FROM microsoft/windowsservercore
ADD HangfireConsole/bin/Release/ /
ENTRYPOINT HangfireConsole.exe