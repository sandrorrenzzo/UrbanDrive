using UrbanDrive.Models.Entities;

namespace UrbanDrive.Services;

public class LocacaoService
{
    private readonly List<Locacao> _locacoes = new();
    private readonly MotoristaService _motoristaService;
    private readonly VeiculoService _veiculoService;

    public LocacaoService(MotoristaService motoristaService, VeiculoService veiculoService)
    {
        _motoristaService = motoristaService;
        _veiculoService = veiculoService;
    }

    public Locacao Registrar(string cpf, int idVeiculo, int dias)
    {
        var motorista = _motoristaService.BuscarPorCpf(cpf);

        if (motorista is null)
            throw new ArgumentException("Motorista não encontrado.");

        var veiculo = _veiculoService.BuscarPorId(idVeiculo);

        if (veiculo is null)
            throw new ArgumentException("Veículo não encontrado.");

        var locacao = new Locacao(motorista, veiculo, dias);

        _locacoes.Add(locacao);

        return locacao;
    }

    public IReadOnlyList<Locacao> Listar() => _locacoes.AsReadOnly();
}
