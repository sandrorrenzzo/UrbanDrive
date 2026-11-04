using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities;

// Herda de Carro (portas, seguro) e implementa o contrato IEletrico.
// Uma classe só herda de uma classe, mas pode implementar várias interfaces.
public class CarroEletrico : Carro, IEletrico
{
    private const decimal RecargaPorDia = 10m;

    private readonly Bateria _bateria = new();

    public CarroEletrico(int id, string placa, string modelo, decimal valorDiaria, int quantidadePortas)
        : base(id, placa, modelo, valorDiaria, quantidadePortas)
    {
    }

    public int NivelBateria => _bateria.Nivel;

    public void ConsumirBateria(int dias) => _bateria.ConsumirParaLocacao(dias);

    public void Recarregar() => _bateria.Recarregar();

    // Reaproveita o cálculo do Carro (regra comum + seguro) e soma a recarga diária.
    public override decimal CalcularAluguel(int dias) => base.CalcularAluguel(dias) + RecargaPorDia * dias;

    public override string Tipo => "Carro elétrico";

    public override string GerarRelatorio() =>
        $"{base.GerarRelatorio()}{Environment.NewLine}Bateria: {NivelBateria}%";

    public override string ToString() => $"{base.ToString()} | bateria {NivelBateria}%";
}
