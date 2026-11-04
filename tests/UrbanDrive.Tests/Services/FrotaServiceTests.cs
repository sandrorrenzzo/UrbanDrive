using UrbanDrive.Models.Entities;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Services;

public class FrotaServiceTests
{
    [Fact]
    public void Adicionar_VeiculoCadastrado_EntraNaFrota()
    {
        var veiculos = new VeiculoService();
        var service = new FrotaService(new Frota("Principal"), veiculos);
        var veiculo = veiculos.Cadastrar("ABC1D23", "Onix", 150m);

        service.Adicionar(veiculo.Id);

        Assert.Same(veiculo, service.Listar()[0]);
    }

    [Fact]
    public void Adicionar_VeiculoNaoCadastrado_LancaExcecao()
    {
        var service = new FrotaService(new Frota("Principal"), new VeiculoService());

        Assert.Throws<ArgumentException>(() => service.Adicionar(9999));
    }

    [Fact]
    public void Remover_VeiculoDaFrota_SaiDaFrotaMasContinuaCadastrado()
    {
        var veiculos = new VeiculoService();
        var service = new FrotaService(new Frota("Principal"), veiculos);
        var veiculo = veiculos.Cadastrar("ABC1D23", "Onix", 150m);
        service.Adicionar(veiculo.Id);

        service.Remover(veiculo.Id);

        Assert.Empty(service.Listar());
        Assert.Same(veiculo, veiculos.BuscarPorId(veiculo.Id));
    }

    [Fact]
    public void Remover_VeiculoQueNaoEstaNaFrota_LancaExcecao()
    {
        var veiculos = new VeiculoService();
        var service = new FrotaService(new Frota("Principal"), veiculos);
        var veiculo = veiculos.Cadastrar("ABC1D23", "Onix", 150m);

        Assert.Throws<ArgumentException>(() => service.Remover(veiculo.Id));
    }
}
