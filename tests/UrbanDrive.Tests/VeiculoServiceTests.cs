using UrbanDrive.Services;

namespace UrbanDrive.Tests;

public class VeiculoServiceTests
{
    [Fact]
    public void Cadastrar_ComDadosValidos_AdicionaNaListaComIdSequencial()
    {
        var service = new VeiculoService();

        var primeiro = service.Cadastrar("ABC1D23", "Onix", 150m);
        var segundo = service.Cadastrar("XYZ9K87", "HB20", 140m);

        Assert.Equal(1, primeiro.Id);
        Assert.Equal(2, segundo.Id);
        Assert.Equal(2, service.Listar().Count);
    }

    [Fact]
    public void Cadastrar_ComPlacaVazia_NaoAdicionaNaLista()
    {
        var service = new VeiculoService();

        Assert.Throws<ArgumentException>(() => service.Cadastrar("", "Onix", 150m));
        Assert.Empty(service.Listar());
    }
}
