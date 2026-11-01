using UrbanDrive.Models.Entities;

namespace UrbanDrive.Services;

public class VeiculoService
{
    private readonly List<Veiculo> _veiculos = new();
    private int _proximoId = 1;

    public Veiculo Cadastrar(string placa, string modelo, decimal valorDiaria)
    {
        var veiculo = new Veiculo(_proximoId, placa, modelo, valorDiaria);
        _veiculos.Add(veiculo);
        _proximoId++;
        return veiculo;
    }

    public IReadOnlyList<Veiculo> Listar() => _veiculos.AsReadOnly();
}
