using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Services;

public class RelatorioServiceTests
{
    [Fact]
    public void GerarTodos_IncluiVeiculosMotoristasFrotaELocacoes()
    {
        var veiculos = new VeiculoService();
        var motoristas = new MotoristaService();
        var frota = new FrotaService(new Frota("Principal"), veiculos);
        var locacoes = new LocacaoService(motoristas, veiculos);
        var relatorios = new RelatorioService(veiculos, motoristas, frota, locacoes);

        var carro = veiculos.CadastrarCarro("ABC1D23", "Onix", 150m, 4);
        veiculos.CadastrarPatinete("PAT0001", "Xiaomi M365", 30m, 25);
        motoristas.Cadastrar("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B,
            DateOnly.FromDateTime(DateTime.Today.AddYears(2)));
        frota.Adicionar(carro.Id);
        locacoes.Registrar("12345678909", carro.Id, 2);

        var textos = relatorios.GerarTodos();

        // 2 veículos + 1 motorista + 1 frota + 1 locação
        Assert.Equal(5, textos.Count);
        Assert.Contains(textos, t => t.Contains("Relatório do veículo"));
        Assert.Contains(textos, t => t.Contains("Relatório do motorista"));
        Assert.Contains(textos, t => t.Contains("Relatório da frota Principal"));
        Assert.Contains(textos, t => t.Contains("Recibo de locação"));
    }

    [Fact]
    public void GerarTodos_SemCadastros_TemSoAFrotaVazia()
    {
        var veiculos = new VeiculoService();
        var motoristas = new MotoristaService();
        var relatorios = new RelatorioService(veiculos, motoristas,
            new FrotaService(new Frota("Principal"), veiculos), new LocacaoService(motoristas, veiculos));

        var textos = relatorios.GerarTodos();

        Assert.Single(textos);
        Assert.Contains("Quantidade de veículos: 0", textos[0]);
    }
}
