using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;

namespace UrbanDrive.Tests.Models.Entities;

public class MotoristaTests
{
    private static readonly DateOnly ValidadeFutura = DateOnly.FromDateTime(DateTime.Today.AddYears(2));

    [Fact]
    public void Construtor_ComDadosValidos_CriaMotoristaComCnhVinculada()
    {
        var motorista = new Motorista("Ana Souza", "123.456.789-09", "12345678900", CategoriaCnh.B, ValidadeFutura);

        Assert.Equal("Ana Souza", motorista.Nome);
        Assert.Equal("12345678909", motorista.Cpf);
        Assert.NotNull(motorista.Cnh);
        Assert.Equal("12345678900", motorista.Cnh.Numero);
        Assert.Equal(CategoriaCnh.B, motorista.Cnh.Categoria);
        Assert.Equal(ValidadeFutura, motorista.Cnh.Validade);
    }

    [Fact]
    public void Cnh_NaoPodeSerCriadaForaDoProjetoModels()
    {
        Assert.Empty(typeof(CNH).GetConstructors());
    }

    [Fact]
    public void Cnh_NaoPodeSerTrocadaDepoisDeCriada()
    {
        Assert.False(typeof(Motorista).GetProperty(nameof(Motorista.Cnh))!.CanWrite);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_ComNomeVazio_LancaExcecao(string? nome)
    {
        Assert.Throws<ArgumentException>(() =>
            new Motorista(nome!, "12345678909", "12345678900", CategoriaCnh.B, ValidadeFutura));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    public void Construtor_ComCpfInvalido_LancaExcecao(string? cpf)
    {
        Assert.Throws<ArgumentException>(() =>
            new Motorista("Ana Souza", cpf!, "12345678900", CategoriaCnh.B, ValidadeFutura));
    }

    [Fact]
    public void Construtor_ComNumeroCnhVazio_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            new Motorista("Ana Souza", "12345678909", "", CategoriaCnh.B, ValidadeFutura));
    }

    [Fact]
    public void Construtor_ComCategoriaInvalida_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            new Motorista("Ana Souza", "12345678909", "12345678900", (CategoriaCnh)99, ValidadeFutura));
    }

    [Fact]
    public void Construtor_ComCnhVencida_LancaExcecao()
    {
        var ontem = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

        Assert.Throws<ArgumentException>(() =>
            new Motorista("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, ontem));
    }
}
