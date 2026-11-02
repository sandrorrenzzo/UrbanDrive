using UrbanDrive.Models.Enums;

namespace UrbanDrive.Models.Entities;

public class CNH
{
    private readonly string _numero;
    private readonly CategoriaCnh _categoria;
    private readonly DateOnly _validade;

    // internal: só pode ser criada dentro do projeto Models, pelo Motorista (composição).
    internal CNH(string numero, CategoriaCnh categoria, DateOnly validade)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("O número da CNH não pode ser nulo ou vazio.");

        if (!Enum.IsDefined(categoria))
            throw new ArgumentException("Categoria da CNH inválida.");

        if (validade < DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("A CNH está vencida.");

        _numero = numero.Trim();
        _categoria = categoria;
        _validade = validade;
    }

    public string Numero => _numero;

    public CategoriaCnh Categoria => _categoria;

    public DateOnly Validade => _validade;

    public override string ToString() =>
        $"CNH {Numero} | Categoria {Categoria} | Validade {Validade:dd/MM/yyyy}";
}
