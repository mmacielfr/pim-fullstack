@echo off
cd /d "%~dp0"
set RID=%1
if "%RID%"=="" set RID=win-x64
echo Gerando executavel autocontido para: %RID%
dotnet publish -c Release -r %RID% --self-contained true ^
  -p:PublishSingleFile=true ^
  -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:DebugType=none ^
  -p:DebugSymbols=false ^
  -o "publish\%RID%"
echo Pronto. Pasta: publish\%RID%\
dir "publish\%RID%"
