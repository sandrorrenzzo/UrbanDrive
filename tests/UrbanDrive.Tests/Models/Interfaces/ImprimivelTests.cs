using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Tests.Models.Interfaces;

public class ImprimivelTests : IDisposable
{
    private readonly decimal _taxaOriginal;

    public ImprimivelTests()
    {
        _taxaOriginal = ConfiguradorSistema.TaxaServico;
        ConfiguradorSistema.TaxaServico = 0.10m;
    }

    public void Dispose() => ConfiguradorSistema.TaxaServico = _taxaOriginal;

    private static Motorista CriarMotorista() =>
        new("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B, new DateOnly(2028, 3, 15));

    [Fact]
    public void Carro_RelatorioTemDadosComunsEPortas()
    {
        var carro = new Carro(1, "ABC1D23", "Onix", 150m, 4);
        carro.RealizarManutencao();

        var relatorio = carro.GerarRelatorio();

        Assert.Contains("Tipo: Carro", relatorio);
        Assert.Contains("Placa: ABC1D23", relatorio);
        Assert.Contains("Manutenções registradas: 1", relatorio);
        Assert.Contains("Portas: 4", relatorio);
    }

    [Fact]
    public void Patinete_RelatorioTemAutonomia()
    {
        var patinete = new Patinete(1, "PAT0001", "Xiaomi M365", 30m, 25);

        var relatorio = patinete.GerarRelatorio();

        Assert.Contains("Tipo: Patinete", relatorio);
        Assert.Contains("Autonomia: 25 km", relatorio);
        Assert.DoesNotContain("Portas", relatorio);
    }

    [Fact]
    public void Motorista_RelatorioIncluiACnh()
    {
        var relatorio = CriarMotorista().GerarRelatorio();

        Assert.Contains("Nome: Ana Souza", relatorio);
        Assert.Contains("CPF: 12345678909", relatorio);
        Assert.Contains("CNH: 12345678900 | Categoria B | Validade 15/03/2028", relatorio);
    }

    [Fact]
    public void Frota_RelatorioListaOsVeiculos()
    {
        var frota = new Frota("Centro");
        frota.Adicionar(new Carro(1, "ABC1D23", "Onix", 150m, 4));
        frota.Adicionar(new Patinete(2, "PAT0001", "Xiaomi M365", 30m, 25));

        var relatorio = frota.GerarRelatorio();

        Assert.Contains("Relatório da frota Centro", relatorio);
        Assert.Contains("Quantidade de veículos: 2", relatorio);
        Assert.Contains("ABC1D23", relatorio);
        Assert.Contains("PAT0001", relatorio);
    }

    [Fact]
    public void Locacao_RelatorioMostraOValorCalculadoPeloTipoDoVeiculo()
    {
        var locacao = new Locacao(CriarMotorista(), new Carro(1, "ABC1D23", "Onix", 150m, 4), 3);

        var relatorio = locacao.GerarRelatorio();

        Assert.Contains("Motorista: Ana Souza", relatorio);
        Assert.Contains("Veículo: Carro ABC1D23 (Onix)", relatorio);
        Assert.Contains("Dias: 3", relatorio);
        Assert.Contains($"Valor total: {555m:C}", relatorio);
    }

    [Fact]
    public void ListaDeImprimiveis_GeraRelatorioDeEntidadesDiferentesSemVerificarOTipo()
    {
        var motorista = CriarMotorista();
        var carro = new Carro(1, "ABC1D23", "Onix", 150m, 4);
        var frota = new Frota("Centro");
        frota.Adicionar(carro);

        var imprimiveis = new List<IImprimivel> { carro, motorista, frota, new Locacao(motorista, carro, 2) };

        var relatorios = imprimiveis.Select(i => i.GerarRelatorio()).ToList();

        Assert.Equal(4, relatorios.Count);
        Assert.All(relatorios, r => Assert.StartsWith("===", r));
    }
}
