using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Services;

// Junta tudo o que pode ser impresso. Só conhece o contrato IImprimivel, não os tipos de cada entidade.
public class RelatorioService
{
    private readonly VeiculoService _veiculoService;
    private readonly MotoristaService _motoristaService;
    private readonly FrotaService _frotaService;
    private readonly LocacaoService _locacaoService;

    public RelatorioService(
        VeiculoService veiculoService,
        MotoristaService motoristaService,
        FrotaService frotaService,
        LocacaoService locacaoService)
    {
        _veiculoService = veiculoService;
        _motoristaService = motoristaService;
        _frotaService = frotaService;
        _locacaoService = locacaoService;
    }

    public IReadOnlyList<IImprimivel> ListarImprimiveis()
    {
        var imprimiveis = new List<IImprimivel>();
        imprimiveis.AddRange(_veiculoService.Listar());
        imprimiveis.AddRange(_motoristaService.Listar());
        imprimiveis.Add(_frotaService.Frota);
        imprimiveis.AddRange(_locacaoService.Listar());
        return imprimiveis.AsReadOnly();
    }

    public IReadOnlyList<string> GerarTodos() => ListarImprimiveis().Select(i => i.GerarRelatorio()).ToList();
}
