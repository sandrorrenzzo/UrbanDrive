using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities;

// abstract: Veiculo é só o modelo comum; os objetos criados são sempre de um tipo concreto
// (Carro, Patinete ou CarroEletrico).
public abstract class Veiculo : IImprimivel
{
    private readonly int _id;
    private string _placa = string.Empty;
    private string _modelo = string.Empty;
    private decimal _valorDiaria;
    private readonly List<string> _historicoManutencoes = new();

    // protected: só as classes filhas chamam este construtor, por meio de : base(...).
    protected Veiculo(int id, string placa, string modelo, decimal valorDiaria)
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

    // Membro abstrato: Veiculo não sabe o próprio tipo; cada classe filha é obrigada a informar.
    public abstract string Tipo { get; }

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

    // IImprimivel: virtual para que cada tipo de veículo acrescente as suas características.
    public virtual string GerarRelatorio() => string.Join(Environment.NewLine,
        $"=== Relatório do veículo #{Id} ===",
        $"Tipo: {Tipo}",
        $"Placa: {Placa}",
        $"Modelo: {Modelo}",
        $"Diária: {ValorDiaria:C}",
        $"Manutenções registradas: {_historicoManutencoes.Count}");

    public override string ToString() =>
        $"#{Id} | {Tipo} | {Placa} | {Modelo} | Diária: {ValorDiaria:C}";

    private string RegistrarManutencao(string descricao)
    {
        var registro = $"{DateTime.Today:dd/MM/yyyy}: {descricao}";
        _historicoManutencoes.Add(registro);
        return registro;
    }
}
