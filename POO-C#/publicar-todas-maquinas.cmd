@echo off
cd /d "%~dp0"
echo Publica um pacote autocontido por tipo de maquina.
echo.

for %%R in (win-x64 linux-x64 osx-x64 osx-arm64) do (
  echo >>> %%R
  dotnet publish -c Release -r %%R --self-contained true ^
    -p:PublishSingleFile=true ^
    -p:IncludeNativeLibrariesForSelfExtract=true ^
    -p:DebugType=none ^
    -p:DebugSymbols=false ^
    -o "publish\%%R"
)

echo.
echo Pastas em publish\
dir publish
