using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities;

// sealed: nenhuma classe pode herdar de Patinete.
// Herda de Veiculo e implementa o contrato IEletrico.
public sealed class Patinete : Veiculo, IEletrico
{
    private const decimal Desconto = 0.20m;

    private readonly int _autonomiaKm;
    private readonly Bateria _bateria = new();

    // A autonomia é validada antes de chamar o construtor de Veiculo,
    // para que um patinete inválido não seja contado no ConfiguradorSistema.
    public Patinete(int id, string placa, string modelo, decimal valorDiaria, int autonomiaKm)
        : base(ValidarAutonomia(autonomiaKm, id), placa, modelo, valorDiaria)
    {
        _autonomiaKm = autonomiaKm;
    }

    public int AutonomiaKm => _autonomiaKm;

    public int NivelBateria => _bateria.Nivel;

    public void ConsumirBateria(int dias) => _bateria.ConsumirParaLocacao(dias);

    public void Recarregar() => _bateria.Recarregar();

    // Reaproveita a regra comum de Veiculo e aplica o desconto do patinete.
    public override decimal CalcularAluguel(int dias) => base.CalcularAluguel(dias) * (1 - Desconto);

    public override string Tipo => "Patinete";

    public override string GerarRelatorio() =>
        $"{base.GerarRelatorio()}{Environment.NewLine}Autonomia: {AutonomiaKm} km{Environment.NewLine}Bateria: {NivelBateria}%";

    public override string ToString() =>
        $"{base.ToString()} | {AutonomiaKm} km de autonomia | bateria {NivelBateria}%";

    private static int ValidarAutonomia(int autonomiaKm, int id)
    {
        if (autonomiaKm <= 0)
            throw new ArgumentException("A autonomia da bateria deve ser maior que zero.");

        return id;
    }
}
