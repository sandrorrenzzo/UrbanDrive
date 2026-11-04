using UrbanDrive.Models.Configuracao;

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

        // Só chega aqui se todas as validações passaram.
        ConfiguradorSistema.RegistrarVeiculoCriado();
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

    // Regra comum a todos os veículos: diária × dias, acrescido da taxa de serviço.
    // virtual: cada tipo de veículo pode sobrescrever e acrescentar a sua regra.
    public virtual decimal CalcularAluguel(int dias)
    {
        if (dias <= 0)
            throw new ArgumentException("A locação deve ter pelo menos 1 dia.");

        return ValorDiaria * dias * (1 + ConfiguradorSistema.TaxaServico);
    }

    public override string ToString() =>
        $"#{Id} | {Placa} | {Modelo} | Diária: {ValorDiaria:C}";
}
