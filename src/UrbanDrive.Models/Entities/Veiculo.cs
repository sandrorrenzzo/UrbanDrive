using UrbanDrive.Models.Configuracao;

namespace UrbanDrive.Models.Entities;

public class Veiculo
{
    private readonly int _id;
    private string _placa = string.Empty;
    private string _modelo = string.Empty;
    private decimal _valorDiaria;
    private readonly List<string> _historicoManutencoes = new();

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

    public IReadOnlyList<string> HistoricoManutencoes => _historicoManutencoes.AsReadOnly();

    // Sobrecarga: mesmo nome, parâmetros diferentes.
    // Sem parâmetro, registra uma manutenção geral.
    public string RealizarManutencao() => RegistrarManutencao("manutenção geral");

    // Com parâmetro, registra qual peça foi substituída.
    public string RealizarManutencao(string pecaSubstituida)
    {
        if (string.IsNullOrWhiteSpace(pecaSubstituida))
            throw new ArgumentException("A peça substituída não pode ser nula ou vazia.");

        return RegistrarManutencao($"troca de {pecaSubstituida.Trim()}");
    }

    public override string ToString() =>
        $"#{Id} | {Placa} | {Modelo} | Diária: {ValorDiaria:C}";

    private string RegistrarManutencao(string descricao)
    {
        var registro = $"{DateTime.Today:dd/MM/yyyy}: {descricao}";
        _historicoManutencoes.Add(registro);
        return registro;
    }
}
