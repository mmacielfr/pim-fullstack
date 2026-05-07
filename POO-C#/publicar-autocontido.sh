#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"
RID="${1:-linux-x64}"
OUT="publish/${RID}"
echo "Gerando executável autocontido para: ${RID}"
echo "(Inclui o runtime .NET — quem recebe o arquivo não precisa instalar nada.)"
dotnet publish -c Release -r "${RID}" --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:DebugType=none \
  -p:DebugSymbols=false \
  -o "${OUT}"
echo "Pronto. Pasta: ${OUT}/"
ls -la "${OUT}"
