using UrbanDrive.Models.Entities;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Services;

public class VeiculoServiceTests
{
    [Fact]
    public void Cadastrar_ComDadosValidos_AdicionaNaListaComIdSequencial()
    {
        var service = new VeiculoService();

        var primeiro = service.CadastrarCarro("ABC1D23", "Onix", 150m, 4);
        var segundo = service.CadastrarCarro("XYZ9K87", "HB20", 140m, 4);

        Assert.Equal(primeiro.Id + 1, segundo.Id);
        Assert.Equal(2, service.Listar().Count);
    }

    [Fact]
    public void Cadastrar_ComPlacaVazia_NaoAdicionaNaLista()
    {
        var service = new VeiculoService();

        Assert.Throws<ArgumentException>(() => service.CadastrarCarro("", "Onix", 150m, 4));
        Assert.Empty(service.Listar());
    }

    [Fact]
    public void CadastrarCarroEPatinete_EntramNaMesmaListaDeVeiculos()
    {
        var service = new VeiculoService();

        var carro = service.CadastrarCarro("ABC1D23", "Onix", 150m, 4);
        var patinete = service.CadastrarPatinete("PAT0001", "Xiaomi M365", 30m, 25);

        Assert.Equal(new Veiculo[] { carro, patinete }, service.Listar());
        Assert.Equal(carro.Id + 1, patinete.Id);
    }

    [Fact]
    public void CadastrarCarro_ComPortasInvalidas_NaoAdicionaNaLista()
    {
        var service = new VeiculoService();

        Assert.Throws<ArgumentException>(() => service.CadastrarCarro("ABC1D23", "Onix", 150m, 0));
        Assert.Empty(service.Listar());
    }

    [Fact]
    public void RealizarManutencao_PeloId_RegistraNoHistoricoDoVeiculo()
    {
        var service = new VeiculoService();
        var carro = service.CadastrarCarro("ABC1D23", "Onix", 150m, 4);

        service.RealizarManutencao(carro.Id);
        service.RealizarManutencao(carro.Id, "pneu");

        Assert.Equal(2, carro.HistoricoManutencoes.Count);
    }

    [Fact]
    public void RealizarManutencao_ComIdInexistente_LancaExcecao()
    {
        var service = new VeiculoService();

        Assert.Throws<ArgumentException>(() => service.RealizarManutencao(9999));
    }
}
