using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Tests.Models.Interfaces;

public class EletricoTests : IDisposable
{
    private readonly decimal _taxaOriginal;

    public EletricoTests()
    {
        _taxaOriginal = ConfiguradorSistema.TaxaServico;
        ConfiguradorSistema.TaxaServico = 0.10m;
    }

    public void Dispose() => ConfiguradorSistema.TaxaServico = _taxaOriginal;

    private static Motorista CriarMotorista() =>
        new("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, DateOnly.FromDateTime(DateTime.Today.AddYears(2)));

    private static Patinete CriarPatinete() => new(1, "PAT0001", "Xiaomi M365", 30m, 25);

    private static CarroEletrico CriarCarroEletrico() => new(2, "ELE1C23", "Dolphin", 150m, 4);

    [Fact]
    public void PatineteECarroEletrico_ImplementamIEletrico_CarroComumNao()
    {
        Assert.IsAssignableFrom<IEletrico>(CriarPatinete());
        Assert.IsAssignableFrom<IEletrico>(CriarCarroEletrico());
        Assert.False(new Carro(3, "ABC1D23", "Onix", 150m, 4) is IEletrico);
    }

    [Fact]
    public void CarroEletrico_EUmCarro()
    {
        Carro carro = CriarCarroEletrico();

        Assert.Equal(4, carro.QuantidadePortas);
        Assert.Equal("Carro elétrico", carro.Tipo);
    }

    [Fact]
    public void VeiculoEletrico_ComecaComBateriaCheia()
    {
        Assert.Equal(100, CriarPatinete().NivelBateria);
        Assert.Equal(100, CriarCarroEletrico().NivelBateria);
    }

    [Fact]
    public void Locacao_ConsomeDezPorCentoDeBateriaPorDia()
    {
        var patinete = CriarPatinete();

        _ = new Locacao(CriarMotorista(), patinete, 3);

        Assert.Equal(70, patinete.NivelBateria);
    }

    [Fact]
    public void Locacao_NaoDeixaBateriaNegativa()
    {
        var patinete = CriarPatinete();

        _ = new Locacao(CriarMotorista(), patinete, 15);

        Assert.Equal(0, patinete.NivelBateria);
    }

    [Fact]
    public void Locacao_ComBateriaAbaixoDeVintePorCento_EhRecusadaENaoConsome()
    {
        var patinete = CriarPatinete();
        _ = new Locacao(CriarMotorista(), patinete, 9);

        Assert.Throws<InvalidOperationException>(() => new Locacao(CriarMotorista(), patinete, 1));
        Assert.Equal(10, patinete.NivelBateria);
    }

    [Fact]
    public void Recarregar_VoltaParaCemPorCentoEPermiteLocarDeNovo()
    {
        var carro = CriarCarroEletrico();
        _ = new Locacao(CriarMotorista(), carro, 9);

        carro.Recarregar();
        var locacao = new Locacao(CriarMotorista(), carro, 2);

        Assert.Equal(80, carro.NivelBateria);
        Assert.Same(carro, locacao.Veiculo);
    }

    [Fact]
    public void Recarregar_PorUmaListaDeIEletrico_FuncionaParaOsDoisTipos()
    {
        var patinete = CriarPatinete();
        var carro = CriarCarroEletrico();
        patinete.ConsumirBateria(5);
        carro.ConsumirBateria(5);

        foreach (var eletrico in new List<IEletrico> { patinete, carro })
            eletrico.Recarregar();

        Assert.Equal(100, patinete.NivelBateria);
        Assert.Equal(100, carro.NivelBateria);
    }

    [Fact]
    public void CarroEletrico_CalculaCarroMaisDezReaisPorDiaDeRecarga()
    {
        // 150 × 3 × 1,10 = 495 + seguro 20 × 3 = 555 + recarga 10 × 3 = 585
        Assert.Equal(585m, CriarCarroEletrico().CalcularAluguel(3));
    }

    [Fact]
    public void Patinete_MantemOCalculoComDesconto()
    {
        Assert.Equal(79.20m, CriarPatinete().CalcularAluguel(3));
    }

    [Fact]
    public void Relatorio_MostraONivelDaBateria()
    {
        var carro = CriarCarroEletrico();
        carro.ConsumirBateria(2);

        var relatorio = carro.GerarRelatorio();

        Assert.Contains("Tipo: Carro elétrico", relatorio);
        Assert.Contains("Portas: 4", relatorio);
        Assert.Contains("Bateria: 80%", relatorio);
    }
}
