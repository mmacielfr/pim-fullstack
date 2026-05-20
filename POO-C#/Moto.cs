namespace AluguelMoto;

public class Moto
{
    public int Id { get; set; }

    public string Modelo { get; set; } = string.Empty;

    public string Marca { get; set; } = "KTM";

    public int CategoriaId { get; set; }

    public string Placa { get; set; } = string.Empty;

    public decimal PrecoMensal { get; set; }

    public int Cilindrada { get; set; }

    public string Motor { get; set; } = string.Empty;

    public string Estilo { get; set; } = string.Empty;

    public string IdealPara { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
