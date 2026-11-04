using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;

namespace UrbanDrive.Tests.Models.Entities;

// Todos os testes fixam a taxa em 10% e devolvem o valor original no fim.
public class CalcularAluguelTests : IDisposable
{
    private readonly decimal _taxaOriginal;

    public CalcularAluguelTests()
    {
        _taxaOriginal = ConfiguradorSistema.TaxaServico;
        ConfiguradorSistema.TaxaServico = 0.10m;
    }

    public void Dispose() => ConfiguradorSistema.TaxaServico = _taxaOriginal;

    [Fact]
    public void Veiculo_CalculaDiariaVezesDiasMaisTaxa()
    {
        var veiculo = new Veiculo(1, "ABC1D23", "Onix", 150m);

        Assert.Equal(495m, veiculo.CalcularAluguel(3));
    }

    [Fact]
    public void Carro_SomaSeguroDeVinteReaisPorDia()
    {
        var carro = new Carro(1, "ABC1D23", "Onix", 150m, 4);

        // 150 × 3 × 1,10 = 495 + 20 × 3 = 555
        Assert.Equal(555m, carro.CalcularAluguel(3));
    }

    [Fact]
    public void Patinete_AplicaDescontoDeVintePorCento()
    {
        var patinete = new Patinete(1, "PAT0001", "Xiaomi M365", 30m, 25);

        // 30 × 3 × 1,10 = 99 × 0,80 = 79,20
        Assert.Equal(79.20m, patinete.CalcularAluguel(3));
    }

    [Fact]
    public void ListaMistaDeVeiculos_CadaUmUsaOProprioCalculo()
    {
        var veiculos = new List<Veiculo>
        {
            new Veiculo(1, "ABC1D23", "Onix", 150m),
            new Carro(2, "ABC1D23", "Onix", 150m, 4),
            new Patinete(3, "PAT0001", "Xiaomi M365", 30m, 25),
        };

        var valores = veiculos.Select(v => v.CalcularAluguel(3)).ToList();

        Assert.Equal(new[] { 495m, 555m, 79.20m }, valores);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TodosOsTipos_RecusamDiasMenorOuIgualAZero(int dias)
    {
        Veiculo[] veiculos =
        [
            new Veiculo(1, "ABC1D23", "Onix", 150m),
            new Carro(2, "ABC1D23", "Onix", 150m, 4),
            new Patinete(3, "PAT0001", "Xiaomi M365", 30m, 25),
        ];

        foreach (var veiculo in veiculos)
            Assert.Throws<ArgumentException>(() => veiculo.CalcularAluguel(dias));
    }

    [Fact]
    public void Locacao_DeCarroEDePatinete_UsaOCalculoDoTipoReal()
    {
        var motorista = new Motorista("Ana Souza", "12345678909", "12345678900", CategoriaCnh.B,
            DateOnly.FromDateTime(DateTime.Today.AddYears(2)));

        var locacaoCarro = new Locacao(motorista, new Carro(1, "ABC1D23", "Onix", 150m, 4), 3);
        var locacaoPatinete = new Locacao(motorista, new Patinete(2, "PAT0001", "Xiaomi M365", 30m, 25), 3);

        Assert.Equal(555m, locacaoCarro.ValorTotal);
        Assert.Equal(79.20m, locacaoPatinete.ValorTotal);
    }
}
