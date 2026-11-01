namespace UrbanDrive.Models.Entities;

public class Veiculo
{
    private readonly int _id;
    private string _placa = string.Empty;
    private string _modelo = string.Empty;
    private decimal _valorDiaria;

    public Veiculo(int id, string placa, string modelo, decimal valorDiaria)
    {
        if (id <= 0)
            throw new ArgumentException("O identificador deve ser maior que zero.");

        _id = id;
        Placa = placa;
        Modelo = modelo;
        ValorDiaria = valorDiaria;
    }

    public int Id => _id;

    public string Placa
    {
        get => _placa;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A placa não pode ser nula ou vazia.");

            _placa = value.Trim().ToUpperInvariant();
        }
    }

    public string Modelo
    {
        get => _modelo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("O modelo não pode ser nulo ou vazio.");

            _modelo = value.Trim();
        }
    }

    public decimal ValorDiaria
    {
        get => _valorDiaria;
        set
        {
            if (value <= 0)
                throw new ArgumentException("O valor da diária deve ser maior que zero.");

            _valorDiaria = value;
        }
    }

    public override string ToString() =>
        $"#{Id} | {Placa} | {Modelo} | Diária: {ValorDiaria:C}";
}
