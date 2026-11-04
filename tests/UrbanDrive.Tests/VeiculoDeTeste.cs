using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests;

// Veiculo é abstrata e não pode ser instanciada. Esta classe existe só nos testes,
// para verificar o comportamento comum de Veiculo sem as regras de Carro ou Patinete.
public class VeiculoDeTeste : Veiculo
{
    public VeiculoDeTeste(int id, string placa, string modelo, decimal valorDiaria)
        : base(id, placa, modelo, valorDiaria)
    {
    }

    public override string Tipo => "Teste";
}
