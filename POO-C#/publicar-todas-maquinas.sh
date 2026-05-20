#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"

echo "Publica um pacote autocontido por tipo de máquina (RID)."
echo "Quem usa só baixa o ZIP da pasta que combina com o PC dele — não precisa instalar .NET."
echo ""

for RID in win-x64 linux-x64 osx-x64 osx-arm64; do
  OUT="publish/${RID}"
  echo ">>> ${RID}"
  dotnet publish -c Release -r "${RID}" --self-contained true \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:DebugType=none \
    -p:DebugSymbols=false \
    -o "${OUT}"
done

echo ""
echo "Pastas geradas em publish/"
ls -la publish/
