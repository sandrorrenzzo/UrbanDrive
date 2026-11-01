using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests;

public class VeiculoTests
{
    [Fact]
    public void Construtor_ComDadosValidos_CriaVeiculo()
    {
        var veiculo = new Veiculo(1, "ABC1D23", "Onix", 150m);

        Assert.Equal(1, veiculo.Id);
        Assert.Equal("ABC1D23", veiculo.Placa);
        Assert.Equal("Onix", veiculo.Modelo);
        Assert.Equal(150m, veiculo.ValorDiaria);
    }

    [Fact]
    public void Construtor_ComPlacaNula_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Veiculo(1, null!, "Onix", 150m));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_ComPlacaVazia_LancaExcecao(string placa)
    {
        Assert.Throws<ArgumentException>(() => new Veiculo(1, placa, "Onix", 150m));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Construtor_ComModeloVazio_LancaExcecao(string? modelo)
    {
        Assert.Throws<ArgumentException>(() => new Veiculo(1, "ABC1D23", modelo!, 150m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComIdInvalido_LancaExcecao(int id)
    {
        Assert.Throws<ArgumentException>(() => new Veiculo(id, "ABC1D23", "Onix", 150m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Construtor_ComValorDiariaInvalido_LancaExcecao(decimal valor)
    {
        Assert.Throws<ArgumentException>(() => new Veiculo(1, "ABC1D23", "Onix", valor));
    }

    [Fact]
    public void Placa_AlteradaParaVazia_LancaExcecaoEMantemValorAnterior()
    {
        var veiculo = new Veiculo(1, "ABC1D23", "Onix", 150m);

        Assert.Throws<ArgumentException>(() => veiculo.Placa = "");
        Assert.Equal("ABC1D23", veiculo.Placa);
    }

    [Fact]
    public void ValorDiaria_AlteradoParaNegativo_LancaExcecaoEMantemValorAnterior()
    {
        var veiculo = new Veiculo(1, "ABC1D23", "Onix", 150m);

        Assert.Throws<ArgumentException>(() => veiculo.ValorDiaria = -1m);
        Assert.Equal(150m, veiculo.ValorDiaria);
    }
}
