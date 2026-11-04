namespace UrbanDrive.Models.Entities;

public class Carro : Veiculo
{
    private const decimal SeguroPorDia = 20m;

    private readonly int _quantidadePortas;

    // As portas são validadas antes de chamar o construtor de Veiculo,
    // para que um carro inválido não seja contado no ConfiguradorSistema.
    public Carro(int id, string placa, string modelo, decimal valorDiaria, int quantidadePortas)
        : base(ValidarPortas(quantidadePortas, id), placa, modelo, valorDiaria)
    {
        _quantidadePortas = quantidadePortas;
    }

    public int QuantidadePortas => _quantidadePortas;

    // Reaproveita a regra comum de Veiculo e acrescenta o seguro diário do carro.
    public override decimal CalcularAluguel(int dias) => base.CalcularAluguel(dias) + SeguroPorDia * dias;

    public override string ToString() => $"{base.ToString()} | Carro {QuantidadePortas} portas";

    private static int ValidarPortas(int quantidadePortas, int id)
    {
        if (quantidadePortas <= 0)
            throw new ArgumentException("A quantidade de portas deve ser maior que zero.");

        return id;
    }
}
