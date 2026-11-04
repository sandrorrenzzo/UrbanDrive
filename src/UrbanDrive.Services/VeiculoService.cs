using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.Services;

public class VeiculoService
{
    private readonly List<Veiculo> _veiculos = new();

    public Veiculo Cadastrar(string placa, string modelo, decimal valorDiaria)
    {
        var veiculo = new Veiculo(ConfiguradorSistema.TotalVeiculos + 1, placa, modelo, valorDiaria);
        _veiculos.Add(veiculo);
        return veiculo;
    }

    public IReadOnlyList<Veiculo> Listar() => _veiculos.AsReadOnly();

    public Veiculo? BuscarPorId(int id) => _veiculos.FirstOrDefault(v => v.Id == id);
}
