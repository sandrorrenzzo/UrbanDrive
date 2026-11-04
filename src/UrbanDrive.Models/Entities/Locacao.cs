using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities;

public class Locacao : IImprimivel
{
    private readonly Motorista _motorista;
    private readonly Veiculo _veiculo;
    private readonly int _dias;

    public Locacao(Motorista motorista, Veiculo veiculo, int dias)
    {
        if (motorista is null)
            throw new ArgumentException("O motorista é obrigatório.");

        if (veiculo is null)
            throw new ArgumentException("O veículo é obrigatório.");

        if (dias <= 0)
            throw new ArgumentException("A locação deve ter pelo menos 1 dia.");

        _motorista = motorista;
        _veiculo = veiculo;
        _dias = dias;
    }

    public Motorista Motorista => _motorista;
    public Veiculo Veiculo => _veiculo;
    public int Dias => _dias;
    // Polimorfismo: o cálculo usado é o do tipo real do veículo (Carro, Patinete ou CarroEletrico).
    public decimal ValorTotal => _veiculo.CalcularAluguel(_dias);

    // O relatório da locação funciona como o recibo do aluguel.
    public string GerarRelatorio() => string.Join(Environment.NewLine,
        "=== Recibo de locação ===",
        $"Motorista: {Motorista.Nome} (CPF {Motorista.Cpf})",
        $"Veículo: {Veiculo.Tipo} {Veiculo.Placa} ({Veiculo.Modelo})",
        $"Dias: {Dias}",
        $"Valor total: {ValorTotal:C}");

    public override string ToString() =>
        $"{Motorista.Nome} alugou {Veiculo.Placa} ({Veiculo.Modelo}) por {Dias} dia(s) | Total: {ValorTotal:C}";
}
