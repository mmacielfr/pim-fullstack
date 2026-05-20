// Precisamos de List<> (lista dinâmica). Sem esta linha teríamos que escrever o nome completo System.Collections.Generic.List.
using System.Collections.Generic;

// Agrupa as classes do projeto; as outras classes (Cliente, Moto, Categoria) usam o mesmo namespace.
namespace AluguelMoto;

// Classe que o .NET procura para iniciar o programa. O nome "Program" é convenção.
class Program
{
    // Ponto de entrada: o sistema chama Main automaticamente ao rodar o .exe.
    // "static" = não precisa criar um objeto Program para chamar.
    // "string[] args" = argumentos da linha de comando (não usamos neste exercício, mas o modelo pede).
    static void Main(string[] args)
    {
        // ========== PARTE 1: montar os dados de mentirinha (cadastro fixo no código) ==========
        // Por quê List? Porque podemos percorrer com foreach e adicionar vários itens sem fixar tamanho na mão.
        // Por quê não gravar em arquivo? O enunciado pediu só terminal com dados mockados.

        List<Categoria> categorias = new List<Categoria>();
        // Categorias alinhadas com o front (modelos.html): Off-Road, Naked, Sport.
        categorias.Add(new Categoria { Id = 1, Nome = "Off-Road" });
        categorias.Add(new Categoria { Id = 2, Nome = "Naked" });
        categorias.Add(new Categoria { Id = 3, Nome = "Sport" });

        List<Moto> motos = new List<Moto>();
        // Catálogo KTM igual ao site (off-road.html, naked.html, sport.html).
        motos.Add(new Moto
        {
            Id = 1,
            Modelo = "KTM 525 EXC 2006",
            Marca = "KTM",
            CategoriaId = 1,
            Placa = "KTM1O01",
            PrecoMensal = 2199,
            Cilindrada = 510,
            Motor = "Monocilíndrico 4 tempos",
            Estilo = "Trail / Enduro",
            IdealPara = "Trilhas, terreno misto e uso off-road",
            Descricao = "Moto off-road robusta, com boa suspensão para irregularidades e motor linear para controle em baixa e média rotação."
        });
        motos.Add(new Moto
        {
            Id = 2,
            Modelo = "KTM Duke 390",
            Marca = "KTM",
            CategoriaId = 2,
            Placa = "KTM2N02",
            PrecoMensal = 1499,
            Cilindrada = 373,
            Motor = "Monocilíndrico",
            Estilo = "Naked urbana",
            IdealPara = "Cidade, estrada e uso diário",
            Descricao = "Posição ereta, manuseio leve e eletrônica moderna. Ótima para deslocamentos urbanos com visual esportivo."
        });
        motos.Add(new Moto
        {
            Id = 3,
            Modelo = "KTM RC 390",
            Marca = "KTM",
            CategoriaId = 3,
            Placa = "KTM3S03",
            PrecoMensal = 1799,
            Cilindrada = 373,
            Motor = "Monocilíndrico",
            Estilo = "Sport carenada",
            IdealPara = "Pista, estrada e pilotagem esportiva",
            Descricao = "Potência, freios de alto desempenho e aerodinâmica pensada para estabilidade em velocidade."
        });

        // ========== PARTE 2: tela inicial e primeira pergunta ==========

        // string? = o texto pode não existir se ReadLine() vier nulo; o compilador do C# moderno exige isso.
        string? linha;

        // WriteLine pula linha no final; Write deixa o cursor na mesma linha para o usuário responder ao lado.
        Console.WriteLine("========================================");
        Console.WriteLine("   Bem-vindo ao aluguel de motos KTM!");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.Write("Gostaria de alugar uma moto? (s/n): ");
        linha = Console.ReadLine();
        // Montamos uma string só com a resposta normalizada: sem espaços nas pontas e em minúsculas,
        // para aceitar "S", "s", " SIM " etc. de forma previsível.
        string querAlugar = "";
        if (linha != null)
        {
            querAlugar = linha.Trim().ToLower();
        }

        // Se não for "s" nem "sim", não faz sentido continuar o fluxo de aluguel.
        if (querAlugar != "s" && querAlugar != "sim")
        {
            Console.WriteLine("Ok. Volte quando quiser!");
            // return sai do Main e termina o programa na hora (não executa o resto do código abaixo).
            return;
        }

        // ========== PARTE 3: mostrar categorias e ler a escolha ==========

        Console.WriteLine();
        Console.WriteLine("--- Categorias ---");
        // foreach percorre todos os itens da lista sem precisar de índice [0], [1]...
        foreach (Categoria c in categorias)
        {
            Console.WriteLine("  " + c.Id + ") " + c.Nome);
        }

        Console.WriteLine();
        Console.Write("Escolha o número da categoria: ");
        linha = Console.ReadLine();
        // catId começa em 0; se o usuário digitar lixo, TryParse deixa 0 (ou podemos tratar como erro na verificação).
        int catId = 0;
        if (linha != null)
        {
            // TryParse tenta converter texto em número; se falhar, não "quebra" o programa como Convert.ToInt32 faria.
            int.TryParse(linha, out catId);
        }

        // Percorremos de novo as categorias para duas coisas: saber se o Id existe e guardar o nome para o título da próxima tela.
        bool categoriaOk = false;
        string nomeCategoriaEscolhida = "";
        foreach (Categoria c in categorias)
        {
            if (c.Id == catId)
            {
                categoriaOk = true;
                nomeCategoriaEscolhida = c.Nome;
                // break para no primeiro achado; não precisa continuar o laço.
                break;
            }
        }

        if (categoriaOk == false)
        {
            Console.WriteLine("Categoria inválida.");
            return;
        }

        // ========== PARTE 4: filtrar motos só da categoria escolhida ==========
        // Não mostramos todas as motos de uma vez: primeiro o usuário escolhe o tipo (categoria).

        List<Moto> listaMotos = new List<Moto>();
        foreach (Moto m in motos)
        {
            if (m.CategoriaId == catId)
            {
                listaMotos.Add(m);
            }
        }

        // Por segurança: se alguém mudar os dados e ficar categoria sem moto, evitamos erro nas próximas telas.
        if (listaMotos.Count == 0)
        {
            Console.WriteLine("Não há motos nesta categoria.");
            return;
        }

        // ========== PARTE 5: listar motos da categoria e ler a escolha ==========

        Console.WriteLine();
        // Mostramos o nome da categoria no título para o usuário não se perder.
        Console.WriteLine("--- Motos (" + nomeCategoriaEscolhida + ") ---");
        foreach (Moto m in listaMotos)
        {
            Console.WriteLine("  " + m.Id + ") " + m.Marca + " " + m.Modelo + " - R$ " + m.PrecoMensal + "/mês - Placa " + m.Placa);
        }

        Console.WriteLine();
        Console.Write("Escolha o número da moto: ");
        linha = Console.ReadLine();
        int motoId = 0;
        if (linha != null)
        {
            int.TryParse(linha, out motoId);
        }

        // Validamos contra listaMotos (o que foi mostrado), não contra todas as motos: assim o número tem que ser uma das opções visíveis.
        bool motoOk = false;
        foreach (Moto m in listaMotos)
        {
            if (m.Id == motoId)
            {
                motoOk = true;
                break;
            }
        }

        if (motoOk == false)
        {
            Console.WriteLine("Moto inválida.");
            return;
        }

        // Buscamos na lista completa "motos" para ter o objeto certo com modelo e placa no resumo final.
        // (listaMotos já teria a mesma moto, mas repetimos na lista geral para ficar explícito que vem do cadastro principal.)
        Moto motoEscolhida = new Moto();
        foreach (Moto m in motos)
        {
            if (m.Id == motoId)
            {
                motoEscolhida = m;
                break;
            }
        }

        // ========== PARTE 6: formulário do cliente (objeto Cliente guarda tudo junto) ==========
        // Por quê objeto? É POO: agrupar nome, telefone, email e cpf num só lugar.

        Console.WriteLine();
        Console.WriteLine("--- Formulário do locatário ---");

        Cliente cliente = new Cliente();

        Console.Write("Nome completo: ");
        linha = Console.ReadLine();
        // Se ReadLine for nulo, guardamos string vazia para não dar erro ao usar cliente.Nome depois.
        if (linha != null)
            cliente.Nome = linha.Trim();
        else
            cliente.Nome = "";

        Console.Write("Telefone: ");
        linha = Console.ReadLine();
        if (linha != null)
            cliente.Telefone = linha.Trim();
        else
            cliente.Telefone = "";

        Console.Write("E-mail: ");
        linha = Console.ReadLine();
        if (linha != null)
            cliente.Email = linha.Trim();
        else
            cliente.Email = "";

        Console.Write("CPF: ");
        linha = Console.ReadLine();
        if (linha != null)
            cliente.Cpf = linha.Trim();
        else
            cliente.Cpf = "";

        // ========== PARTE 7: confirmar o aluguel (última chance de cancelar) ==========

        Console.WriteLine();
        Console.WriteLine("Dados preenchidos. Deseja confirmar o aluguel?");
        Console.WriteLine("  1) Alugar");
        Console.WriteLine("  2) Cancelar");
        Console.Write("Opção: ");
        linha = Console.ReadLine();
        string opcao = "";
        if (linha != null)
            opcao = linha.Trim();

        // Qualquer coisa diferente de "1" é tratada como cancelamento (inclusive Enter vazio ou "2").
        if (opcao != "1")
        {
            Console.WriteLine("Aluguel cancelado.");
            return;
        }

        // ========== PARTE 8: registrar aluguel e mensagem final de sucesso ==========

        Aluguel aluguel = new Aluguel
        {
            Id = 1,
            Moto = motoEscolhida,
            Cliente = cliente,
            NomeCategoria = nomeCategoriaEscolhida,
            ValorMensal = motoEscolhida.PrecoMensal,
            DataAluguel = DateTime.Now
        };

        Console.WriteLine();
        Console.WriteLine(">>> Moto alugada com sucesso! <<<");
        Console.WriteLine("    Cliente: " + aluguel.Cliente.Nome);
        Console.WriteLine("    Moto: " + aluguel.Moto.Marca + " " + aluguel.Moto.Modelo + " (" + aluguel.Moto.Placa + ")");
        Console.WriteLine("    Categoria: " + aluguel.NomeCategoria);
        Console.WriteLine("    Valor: R$ " + aluguel.ValorMensal + " / mês");
        Console.WriteLine("    Cilindrada: " + aluguel.Moto.Cilindrada + " cm³");
        Console.WriteLine("    Estilo: " + aluguel.Moto.Estilo);
        Console.WriteLine();
        Console.WriteLine("Obrigado por alugar sua KTM. READY TO RACE!");
        // Fim do Main: programa termina naturalmente aqui se chegou até o final.
    }
}
