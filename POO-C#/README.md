# Aluguel Moto (console)

## Importante sobre C#

Programas em C# precisam do **runtime .NET** para rodar. Não existe “rodar C# sem nada no PC”, exceto duas situações:

1. **Instalar o .NET 8** no computador e usar `dotnet run` (bom para desenvolvimento).
2. **Gerar um executável autocontido**: um único pacote que **já leva o runtime dentro**. Quem recebe **não precisa instalar** o .NET — só executar o arquivo (no Linux pode precisar dar permissão: `chmod +x AluguelMoto`).

Quem **gera** esse pacote precisa do [.NET 8 SDK](https://dotnet.microsoft.com/download) **uma vez** (na sua máquina ou na do professor).

---

## Desenvolvimento (com SDK instalado)

```bash
cd POO-C#
dotnet run
```

---

## Gerar versão para distribuir (sem pedir .NET no PC de quem vai usar)

### Linux (script)

```bash
cd POO-C#
chmod +x publicar-autocontido.sh
./publicar-autocontido.sh linux-x64
```

O programa sai em `publish/linux-x64/` — envie essa pasta (ou compacte em `.zip`).

### Windows (script)

```cmd
cd POO-C#
publicar-autocontido.cmd win-x64
```

Saída em `publish\win-x64\` — envie a pasta ou um `.zip` com `AluguelMoto.exe` e os arquivos ao lado.

### Manual (qualquer SO)

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ./publish/linux-x64
```

No Windows, troque `linux-x64` por `win-x64`.

### Rodar em “todas as máquinas” (sem instalar .NET no PC de quem usa)

Um único arquivo não serve para Windows **e** Linux **e** Mac ao mesmo tempo (são sistemas diferentes). O jeito padrão é gerar **um pacote por tipo de PC**:

| Pasta gerada | Para quem |
|--------------|-----------|
| `publish/win-x64` | Windows 64 bits (maioria dos PCs) |
| `publish/linux-x64` | Linux 64 bits (Ubuntu, etc.) |
| `publish/osx-x64` | Mac Intel |
| `publish/osx-arm64` | Mac Apple Silicon (M1/M2/M3) |

Com o **SDK instalado só na sua máquina**, rode:

```bash
cd POO-C#
chmod +x publicar-todas-maquinas.sh
./publicar-todas-maquinas.sh
```

No Windows:

```cmd
cd POO-C#
publicar-todas-maquinas.cmd
```

Depois compacte cada pasta em um `.zip` e envie: cada pessoa usa só o zip do sistema dela (sem instalar .NET).

---

Pastas `bin`, `obj` e `publish` são geradas na compilação/publicação e ficam no `.gitignore` do repositório.
