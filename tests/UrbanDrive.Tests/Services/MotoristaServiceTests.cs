using UrbanDrive.Models.Enums;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Services;

public class MotoristaServiceTests
{
    private static readonly DateOnly ValidadeFutura = DateOnly.FromDateTime(DateTime.Today.AddYears(2));

    [Fact]
    public void Cadastrar_ComDadosValidos_AdicionaNaLista()
    {
        var service = new MotoristaService();

        var motorista = service.Cadastrar("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, ValidadeFutura);

        Assert.Single(service.Listar());
        Assert.Same(motorista, service.Listar()[0]);
    }

    [Fact]
    public void Cadastrar_ComDadosInvalidos_NaoAdicionaNaLista()
    {
        var service = new MotoristaService();

        Assert.Throws<ArgumentException>(() =>
            service.Cadastrar("", "12345678909", "12345678900", CategoriaCnh.B, ValidadeFutura));
        Assert.Empty(service.Listar());
    }

    [Theory]
    [InlineData("12345678909")]
    [InlineData("123.456.789-09")]
    public void BuscarPorCpf_ComOuSemPontuacao_EncontraOMotorista(string cpfDigitado)
    {
        var service = new MotoristaService();
        var motorista = service.Cadastrar("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, ValidadeFutura);

        Assert.Same(motorista, service.BuscarPorCpf(cpfDigitado));
    }

    [Fact]
    public void BuscarPorCpf_Inexistente_RetornaNulo()
    {
        var service = new MotoristaService();

        Assert.Null(service.BuscarPorCpf("98765432100"));
    }
}
