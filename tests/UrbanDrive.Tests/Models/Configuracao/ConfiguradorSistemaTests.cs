using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Models.Configuracao;

public class ConfiguradorSistemaTests
{
    [Fact]
    public void Contador_AumentaUmACadaVeiculoCriado()
    {
        var antes = ConfiguradorSistema.TotalVeiculos;

        _ = new Veiculo(1, "ABC1D23", "Onix", 150m);
        _ = new Veiculo(2, "XYZ9K87", "HB20", 140m);

        Assert.Equal(antes + 2, ConfiguradorSistema.TotalVeiculos);
    }

    [Fact]
    public void Contador_NaoAumentaQuandoCriacaoFalha()
    {
        var antes = ConfiguradorSistema.TotalVeiculos;

        Assert.Throws<ArgumentException>(() => new Veiculo(1, "", "Onix", 150m));

        Assert.Equal(antes, ConfiguradorSistema.TotalVeiculos);
    }

    [Fact]
    public void Contador_NaoPodeSerAlteradoDeFora()
    {
        Assert.False(typeof(ConfiguradorSistema).GetProperty(nameof(ConfiguradorSistema.TotalVeiculos))!.CanWrite);
    }

    [Fact]
    public void Contador_ECompartilhadoEntreServicos()
    {
        var servicoA = new VeiculoService();
        var servicoB = new VeiculoService();

        var primeiro = servicoA.Cadastrar("ABC1D23", "Onix", 150m);
        var segundo = servicoB.Cadastrar("XYZ9K87", "HB20", 140m);

        Assert.Equal(primeiro.Id + 1, segundo.Id);
    }

    [Fact]
    public void Taxa_AlteradaEmUmLugar_EVistaEmTodoOSistema()
    {
        var taxaOriginal = ConfiguradorSistema.TaxaServico;
        try
        {
            ConfiguradorSistema.TaxaServico = 0.25m;

            Assert.Equal(0.25m, LerTaxaEmOutroLugar());
        }
        finally
        {
            ConfiguradorSistema.TaxaServico = taxaOriginal;
        }
    }

    [Fact]
    public void Taxa_Negativa_LancaExcecaoEMantemValorAnterior()
    {
        var taxaOriginal = ConfiguradorSistema.TaxaServico;

        Assert.Throws<ArgumentException>(() => ConfiguradorSistema.TaxaServico = -0.1m);
        Assert.Equal(taxaOriginal, ConfiguradorSistema.TaxaServico);
    }

    private static decimal LerTaxaEmOutroLugar() => ConfiguradorSistema.TaxaServico;
}
