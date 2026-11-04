using UrbanDrive.Models.Entities;

namespace UrbanDrive.Services;

public class FrotaService
{
    private readonly Frota _frota;
    private readonly VeiculoService _veiculoService;

    public FrotaService(Frota frota, VeiculoService veiculoService)
    {
        _frota = frota;
        _veiculoService = veiculoService;
    }

    public Frota Frota => _frota;

    public Veiculo Adicionar(int idVeiculo)
    {
        var veiculo = BuscarVeiculo(idVeiculo);
        _frota.Adicionar(veiculo);
        return veiculo;
    }

    public Veiculo Remover(int idVeiculo)
    {
        var veiculo = BuscarVeiculo(idVeiculo);

        if (!_frota.Remover(veiculo))
            throw new ArgumentException($"O veículo {veiculo.Placa} não está na frota.");

        return veiculo;
    }

    public IReadOnlyList<Veiculo> Listar() => _frota.Listar();

    private Veiculo BuscarVeiculo(int idVeiculo) =>
        _veiculoService.BuscarPorId(idVeiculo) ?? throw new ArgumentException("Veículo não encontrado.");
}
