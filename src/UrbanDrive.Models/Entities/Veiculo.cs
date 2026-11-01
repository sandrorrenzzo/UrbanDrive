namespace UrbanDrive.Models.Entities;

public class Veiculo
{
    private readonly int _id;
    private string _placa = string.Empty;
    private string _modelo = string.Empty;
    private decimal _valorDiaria;

    public Veiculo(int id, string placa, string modelo, decimal valorDiaria)
    {
        _id = id;
        Placa = placa;
        Modelo = modelo;
        ValorDiaria = valorDiaria;
    }

    public int Id => _id;

    public string Placa
    {
        get => _placa;
        set => _placa = value;
    }

    public string Modelo
    {
        get => _modelo;
        set => _modelo = value;
    }

    public decimal ValorDiaria
    {
        get => _valorDiaria;
        set => _valorDiaria = value;
    }

    public override string ToString() =>
        $"#{Id} | {Placa} | {Modelo} | Diária: {ValorDiaria:C}";
}
