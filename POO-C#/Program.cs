using System.Collections.Generic;

namespace AluguelMoto;

class Program
{
    static void Main(string[] args)
    {
        List<Categoria> categorias = new List<Categoria>();
        categorias.Add(new Categoria { Id = 1, Nome = "Off-Road" });
        categorias.Add(new Categoria { Id = 2, Nome = "Naked" });
        categorias.Add(new Categoria { Id = 3, Nome = "Sport" });

        List<Moto> motos = new List<Moto>();
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

        string? linha;

        Console.WriteLine("========================================");
        Console.WriteLine("   Bem-vindo ao aluguel de motos KTM!");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.Write("Gostaria de alugar uma moto? (s/n): ");
        linha = Console.ReadLine();
        string querAlugar = "";
        if (linha != null)
        {
            querAlugar = linha.Trim().ToLower();
        }

        if (querAlugar != "s" && querAlugar != "sim")
        {
            Console.WriteLine("Ok. Volte quando quiser!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("--- Categorias ---");
        foreach (Categoria c in categorias)
        {
            Console.WriteLine("  " + c.Id + ") " + c.Nome);
        }

        Console.WriteLine();
        Console.Write("Escolha o número da categoria: ");
        linha = Console.ReadLine();
        int catId = 0;
        if (linha != null)
        {
            int.TryParse(linha, out catId);
        }

        bool categoriaOk = false;
        string nomeCategoriaEscolhida = "";
        foreach (Categoria c in categorias)
        {
            if (c.Id == catId)
            {
                categoriaOk = true;
                nomeCategoriaEscolhida = c.Nome;
                break;
            }
        }

        if (categoriaOk == false)
        {
            Console.WriteLine("Categoria inválida.");
            return;
        }

        List<Moto> listaMotos = new List<Moto>();
        foreach (Moto m in motos)
        {
            if (m.CategoriaId == catId)
            {
                listaMotos.Add(m);
            }
        }

        if (listaMotos.Count == 0)
        {
            Console.WriteLine("Não há motos nesta categoria.");
            return;
        }

        Console.WriteLine();
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

        Moto motoEscolhida = new Moto();
        foreach (Moto m in motos)
        {
            if (m.Id == motoId)
            {
                motoEscolhida = m;
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("--- Formulário do locatário ---");

        Cliente cliente = new Cliente();

        Console.Write("Nome completo: ");
        linha = Console.ReadLine();
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

        Console.WriteLine();
        Console.WriteLine("Dados preenchidos. Deseja confirmar o aluguel?");
        Console.WriteLine("  1) Alugar");
        Console.WriteLine("  2) Cancelar");
        Console.Write("Opção: ");
        linha = Console.ReadLine();
        string opcao = "";
        if (linha != null)
            opcao = linha.Trim();

        if (opcao != "1")
        {
            Console.WriteLine("Aluguel cancelado.");
            return;
        }

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
    }
}
