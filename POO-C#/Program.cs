using System.Collections.Generic;

namespace AluguelMoto;

class Program
{
    static void Main(string[] args)
    {
        List<Categoria> categorias = new List<Categoria>();
        categorias.Add(new Categoria { Id = 1, Nome = "Street" });
        categorias.Add(new Categoria { Id = 2, Nome = "Trail / Aventura" });
        categorias.Add(new Categoria { Id = 3, Nome = "Esportiva" });

        List<Moto> motos = new List<Moto>();
        motos.Add(new Moto { Id = 1, Modelo = "Honda CG 160", CategoriaId = 1, Placa = "ABC1D23" });
        motos.Add(new Moto { Id = 2, Modelo = "Yamaha Fazer 250", CategoriaId = 1, Placa = "XYZ9K87" });
        motos.Add(new Moto { Id = 3, Modelo = "BMW G 310 GS", CategoriaId = 2, Placa = "TRK4E56" });
        motos.Add(new Moto { Id = 4, Modelo = "Honda XRE 300", CategoriaId = 2, Placa = "MNO2F34" });
        motos.Add(new Moto { Id = 5, Modelo = "Kawasaki Ninja 400", CategoriaId = 3, Placa = "QWE8G12" });
        motos.Add(new Moto { Id = 6, Modelo = "Yamaha MT-03", CategoriaId = 3, Placa = "ASD5H99" });

        string? linha;

        Console.WriteLine("========================================");
        Console.WriteLine("   Bem-vindo ao sistema de aluguel de motos!");
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
            Console.WriteLine("  " + m.Id + ") " + m.Modelo + " - Placa " + m.Placa);
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

        Console.WriteLine();
        Console.WriteLine(">>> Moto alugada com sucesso! <<<");
        Console.WriteLine("    Cliente: " + cliente.Nome);
        Console.WriteLine("    Moto: " + motoEscolhida.Modelo + " (" + motoEscolhida.Placa + ")");
        Console.WriteLine();
        Console.WriteLine("Obrigado por usar nosso sistema.");
    }
}
