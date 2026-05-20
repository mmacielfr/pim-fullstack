namespace AluguelMoto;

// Registro de um aluguel confirmado (mantido em memória durante a execução).
public class Aluguel
{
    public int Id { get; set; }

    public Moto Moto { get; set; } = new Moto();

    public Cliente Cliente { get; set; } = new Cliente();

    public string NomeCategoria { get; set; } = string.Empty;

    public decimal ValorMensal { get; set; }

    public DateTime DataAluguel { get; set; }
}
