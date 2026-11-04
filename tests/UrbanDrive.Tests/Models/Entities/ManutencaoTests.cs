using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests.Models.Entities;

public class ManutencaoTests
{
    private static readonly string Hoje = DateTime.Today.ToString("dd/MM/yyyy");

    [Fact]
    public void RealizarManutencao_SemParametro_RegistraManutencaoGeral()
    {
        var veiculo = new VeiculoDeTeste(1, "ABC1D23", "Onix", 150m);

        var registro = veiculo.RealizarManutencao();

        Assert.Equal($"{Hoje}: manutenção geral", registro);
        Assert.Equal(new[] { registro }, veiculo.HistoricoManutencoes);
    }

    [Fact]
    public void RealizarManutencao_ComPeca_RegistraAPecaSubstituida()
    {
        var veiculo = new VeiculoDeTeste(1, "ABC1D23", "Onix", 150m);

        var registro = veiculo.RealizarManutencao("pastilha de freio");

        Assert.Equal($"{Hoje}: troca de pastilha de freio", registro);
        Assert.Equal(new[] { registro }, veiculo.HistoricoManutencoes);
    }

    [Fact]
    public void AsDuasSobrecargas_AcumulamNoMesmoHistorico()
    {
        var veiculo = new VeiculoDeTeste(1, "ABC1D23", "Onix", 150m);

        veiculo.RealizarManutencao();
        veiculo.RealizarManutencao("pneu");

        Assert.Equal(2, veiculo.HistoricoManutencoes.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RealizarManutencao_ComPecaVazia_LancaExcecaoENaoRegistra(string? peca)
    {
        var veiculo = new VeiculoDeTeste(1, "ABC1D23", "Onix", 150m);

        Assert.Throws<ArgumentException>(() => veiculo.RealizarManutencao(peca!));
        Assert.Empty(veiculo.HistoricoManutencoes);
    }

    [Fact]
    public void Historico_NaoPodeSerAlteradoDeFora()
    {
        var veiculo = new VeiculoDeTeste(1, "ABC1D23", "Onix", 150m);
        veiculo.RealizarManutencao();

        Assert.Throws<NotSupportedException>(() => ((IList<string>)veiculo.HistoricoManutencoes).Clear());
        Assert.Single(veiculo.HistoricoManutencoes);
    }

    [Fact]
    public void CarroEPatinete_HerdamAManutencao()
    {
        var carro = new Carro(1, "ABC1D23", "Onix", 150m, 4);
        var patinete = new Patinete(2, "PAT0001", "Xiaomi M365", 30m, 25);

        carro.RealizarManutencao("óleo");
        patinete.RealizarManutencao("bateria");

        Assert.Single(carro.HistoricoManutencoes);
        Assert.Single(patinete.HistoricoManutencoes);
    }
}
