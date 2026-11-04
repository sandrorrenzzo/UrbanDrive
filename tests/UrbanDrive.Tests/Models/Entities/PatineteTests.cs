using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests.Models.Entities;

public class PatineteTests
{
    [Fact]
    public void Construtor_ComDadosValidos_CriaPatineteComDadosDaBaseEProprios()
    {
        var patinete = new Patinete(1, "PAT0001", "Xiaomi M365", 30m, 25);

        Assert.Equal(1, patinete.Id);
        Assert.Equal("PAT0001", patinete.Placa);
        Assert.Equal("Xiaomi M365", patinete.Modelo);
        Assert.Equal(30m, patinete.ValorDiaria);
        Assert.Equal(25, patinete.AutonomiaKm);
    }

    [Fact]
    public void Patinete_EUmVeiculo()
    {
        Veiculo veiculo = new Patinete(1, "PAT0001", "Xiaomi M365", 30m, 25);

        Assert.IsType<Patinete>(veiculo);
    }

    [Fact]
    public void Patinete_ESelado()
    {
        Assert.True(typeof(Patinete).IsSealed);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Construtor_ComAutonomiaInvalida_LancaExcecao(int autonomia)
    {
        Assert.Throws<ArgumentException>(() => new Patinete(1, "PAT0001", "Xiaomi M365", 30m, autonomia));
    }

    [Fact]
    public void Construtor_ComDiariaInvalida_UsaValidacaoDaBase()
    {
        Assert.Throws<ArgumentException>(() => new Patinete(1, "PAT0001", "Xiaomi M365", 0m, 25));
    }

    [Fact]
    public void Construtor_ComAutonomiaInvalida_NaoIncrementaContador()
    {
        var antes = ConfiguradorSistema.TotalVeiculos;

        Assert.Throws<ArgumentException>(() => new Patinete(1, "PAT0001", "Xiaomi M365", 30m, 0));

        Assert.Equal(antes, ConfiguradorSistema.TotalVeiculos);
    }
}
