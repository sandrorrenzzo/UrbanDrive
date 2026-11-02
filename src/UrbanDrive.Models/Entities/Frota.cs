namespace UrbanDrive.Models.Entities;

public class Frota
{
    private string _nome = string.Empty;

    // Agregação: a frota guarda referências a veículos que existem por conta própria.
    private readonly List<Veiculo> _veiculos = new();

    public Frota(string nome)
    {
        Nome = nome;
    }

    public string Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("O nome da frota não pode ser nulo ou vazio.");

            _nome = value.Trim();
        }
    }

    public int Quantidade => _veiculos.Count;

    public void Adicionar(Veiculo veiculo)
    {
        if (veiculo is null)
            throw new ArgumentException("O veículo não pode ser nulo.");

        if (_veiculos.Contains(veiculo))
            throw new InvalidOperationException($"O veículo {veiculo.Placa} já está na frota.");

        _veiculos.Add(veiculo);
    }

    public bool Remover(Veiculo veiculo) => _veiculos.Remove(veiculo);

    public IReadOnlyList<Veiculo> Listar() => _veiculos.AsReadOnly();

    public override string ToString() => $"Frota {Nome} | {Quantidade} veículo(s)";
}
