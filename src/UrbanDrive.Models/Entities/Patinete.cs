namespace UrbanDrive.Models.Entities;

// sealed: nenhuma classe pode herdar de Patinete.
public sealed class Patinete : Veiculo
{
    private readonly int _autonomiaKm;

    // A autonomia é validada antes de chamar o construtor de Veiculo,
    // para que um patinete inválido não seja contado no ConfiguradorSistema.
    public Patinete(int id, string placa, string modelo, decimal valorDiaria, int autonomiaKm)
        : base(ValidarAutonomia(autonomiaKm, id), placa, modelo, valorDiaria)
    {
        _autonomiaKm = autonomiaKm;
    }

    public int AutonomiaKm => _autonomiaKm;

    public override string ToString() => $"{base.ToString()} | Patinete {AutonomiaKm} km de autonomia";

    private static int ValidarAutonomia(int autonomiaKm, int id)
    {
        if (autonomiaKm <= 0)
            throw new ArgumentException("A autonomia da bateria deve ser maior que zero.");

        return id;
    }
}
