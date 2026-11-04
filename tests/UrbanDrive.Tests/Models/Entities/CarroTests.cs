using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests.Models.Entities;

public class CarroTests
{
    [Fact]
    public void Construtor_ComDadosValidos_CriaCarroComDadosDaBaseEProprios()
    {
        var carro = new Carro(1, "ABC1D23", "Onix", 150m, 4);

        Assert.Equal(1, carro.Id);
        Assert.Equal("ABC1D23", carro.Placa);
        Assert.Equal("Onix", carro.Modelo);
        Assert.Equal(150m, carro.ValorDiaria);
        Assert.Equal(4, carro.QuantidadePortas);
    }

    [Fact]
    public void Carro_EUmVeiculo()
    {
        Veiculo veiculo = new Carro(1, "ABC1D23", "Onix", 150m, 4);

        Assert.IsType<Carro>(veiculo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Construtor_ComPortasInvalidas_LancaExcecao(int portas)
    {
        Assert.Throws<ArgumentException>(() => new Carro(1, "ABC1D23", "Onix", 150m, portas));
    }

    [Fact]
    public void Construtor_ComPlacaVazia_UsaValidacaoDaBase()
    {
        Assert.Throws<ArgumentException>(() => new Carro(1, "", "Onix", 150m, 4));
    }

    [Fact]
    public void Construtor_IncrementaContadorDeVeiculos()
    {
        var antes = ConfiguradorSistema.TotalVeiculos;

        _ = new Carro(1, "ABC1D23", "Onix", 150m, 4);

        Assert.Equal(antes + 1, ConfiguradorSistema.TotalVeiculos);
    }

    [Fact]
    public void Construtor_ComPortasInvalidas_NaoIncrementaContador()
    {
        var antes = ConfiguradorSistema.TotalVeiculos;

        Assert.Throws<ArgumentException>(() => new Carro(1, "ABC1D23", "Onix", 150m, 0));

        Assert.Equal(antes, ConfiguradorSistema.TotalVeiculos);
    }
}
