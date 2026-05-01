namespace AluguelMoto;

// Uma moto que pode ser alugada.
public class Moto
{
    // Número que o usuário escolhe no menu.
    public int Id { get; set; }

    // Nome do modelo.
    public string Modelo { get; set; } = string.Empty;

    // Diz de qual categoria é (mesmo número da categoria).
    public int CategoriaId { get; set; }

    // Placa só para mostrar no final (dados de mentirinha).
    public string Placa { get; set; } = string.Empty;
}
