using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;

namespace UrbanDrive.Tests.Models.Entities;

public class LocacaoTests
{
    private static Motorista CriarMotorista() =>
        new("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, DateOnly.FromDateTime(DateTime.Today.AddYears(2)));

    private static Veiculo CriarHB20() => new(1, "XYZ9K87", "HB20", 140m);

    [Fact]
    public void Construtor_ComDadosValidos_GuardaMotoristaVeiculoEDias()
    {
        var motorista = CriarMotorista();
        var veiculo = CriarHB20();

        var locacao = new Locacao(motorista, veiculo, 2);

        Assert.Same(motorista, locacao.Motorista);
        Assert.Same(veiculo, locacao.Veiculo);
        Assert.Equal(2, locacao.Dias);
    }

    [Fact]
    public void ValorTotal_HB20PorDoisDias_ComTaxaDeDezPorCento_Da308()
    {
        var taxaOriginal = ConfiguradorSistema.TaxaServico;
        try
        {
            ConfiguradorSistema.TaxaServico = 0.10m;

            var locacao = new Locacao(CriarMotorista(), CriarHB20(), 2);

            Assert.Equal(308m, locacao.ValorTotal);
        }
        finally
        {
            ConfiguradorSistema.TaxaServico = taxaOriginal;
        }
    }

    [Fact]
    public void ValorTotal_AcompanhaMudancaNaTaxaDoConfiguradorSistema()
    {
        var taxaOriginal = ConfiguradorSistema.TaxaServico;
        try
        {
            ConfiguradorSistema.TaxaServico = 0.10m;
            var locacao = new Locacao(CriarMotorista(), CriarHB20(), 2);

            ConfiguradorSistema.TaxaServico = 0.20m;

            Assert.Equal(336m, locacao.ValorTotal);
        }
        finally
        {
            ConfiguradorSistema.TaxaServico = taxaOriginal;
        }
    }

    [Fact]
    public void Construtor_ComMotoristaNulo_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Locacao(null!, CriarHB20(), 2));
    }

    [Fact]
    public void Construtor_ComVeiculoNulo_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Locacao(CriarMotorista(), null!, 2));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComDiasMenorOuIgualAZero_LancaExcecao(int dias)
    {
        Assert.Throws<ArgumentException>(() => new Locacao(CriarMotorista(), CriarHB20(), dias));
    }

    [Fact]
    public void Locacao_NaoEDonaDoMotoristaNemDoVeiculo()
    {
        var motorista = CriarMotorista();
        var veiculo = CriarHB20();

        var primeira = new Locacao(motorista, veiculo, 2);
        var segunda = new Locacao(motorista, veiculo, 5);

        Assert.Same(primeira.Motorista, segunda.Motorista);
        Assert.Same(primeira.Veiculo, segunda.Veiculo);
    }

    [Fact]
    public void Integracao_MotoristaFrotaELocacao_FuncionamJuntos()
    {
        var taxaOriginal = ConfiguradorSistema.TaxaServico;
        try
        {
            ConfiguradorSistema.TaxaServico = 0.10m;
            var totalAntes = ConfiguradorSistema.TotalVeiculos;

            var motorista = CriarMotorista();
            var frota = new Frota("Centro");
            var onix = new Veiculo(1, "ABC1D23", "Onix", 150m);
            var hb20 = CriarHB20();
            frota.Adicionar(onix);
            frota.Adicionar(hb20);

            var locacao = new Locacao(motorista, frota.Listar()[0], 3);

            Assert.Equal(2, frota.Quantidade);
            Assert.Equal(totalAntes + 2, ConfiguradorSistema.TotalVeiculos);
            Assert.Same(onix, locacao.Veiculo);
            Assert.Equal(495m, locacao.ValorTotal);
        }
        finally
        {
            ConfiguradorSistema.TaxaServico = taxaOriginal;
        }
    }
}
