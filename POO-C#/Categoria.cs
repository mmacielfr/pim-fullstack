namespace AluguelMoto;

// Tipo de moto (ex.: Street), para agrupar as motos no menu.
public class Categoria
{
    // Número que o usuário digita no menu.
    public int Id { get; set; }

    // Nome que aparece na tela.
    public string Nome { get; set; } = string.Empty;
}
