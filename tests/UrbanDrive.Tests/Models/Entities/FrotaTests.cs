using UrbanDrive.Models.Entities;

namespace UrbanDrive.Tests.Models.Entities;

public class FrotaTests
{
    private static Veiculo CriarVeiculo(int id, string placa) => new(id, placa, "Onix", 150m);

    [Fact]
    public void Adicionar_VeiculoValido_ApareceNaListagem()
    {
        var frota = new Frota("Centro");
        var veiculo = CriarVeiculo(1, "ABC1D23");

        frota.Adicionar(veiculo);

        Assert.Single(frota.Listar());
        Assert.Same(veiculo, frota.Listar()[0]);
        Assert.Equal(1, frota.Quantidade);
    }

    [Fact]
    public void Remover_VeiculoDaFrota_RetiraDaListagemMasVeiculoContinuaExistindo()
    {
        var frota = new Frota("Centro");
        var veiculo = CriarVeiculo(1, "ABC1D23");
        frota.Adicionar(veiculo);

        var removido = frota.Remover(veiculo);

        Assert.True(removido);
        Assert.Empty(frota.Listar());
        Assert.Equal("ABC1D23", veiculo.Placa);
    }

    [Fact]
    public void Remover_VeiculoQueNaoEstaNaFrota_RetornaFalse()
    {
        var frota = new Frota("Centro");

        Assert.False(frota.Remover(CriarVeiculo(1, "ABC1D23")));
    }

    [Fact]
    public void Adicionar_MesmoVeiculoDuasVezes_LancaExcecao()
    {
        var frota = new Frota("Centro");
        var veiculo = CriarVeiculo(1, "ABC1D23");
        frota.Adicionar(veiculo);

        Assert.Throws<InvalidOperationException>(() => frota.Adicionar(veiculo));
        Assert.Equal(1, frota.Quantidade);
    }

    [Fact]
    public void Adicionar_VeiculoNulo_LancaExcecao()
    {
        var frota = new Frota("Centro");

        Assert.Throws<ArgumentException>(() => frota.Adicionar(null!));
    }

    [Fact]
    public void Listar_NaoPermiteAlterarAListaDeFora()
    {
        var frota = new Frota("Centro");
        frota.Adicionar(CriarVeiculo(1, "ABC1D23"));

        var lista = frota.Listar();

        Assert.Throws<NotSupportedException>(() => ((IList<Veiculo>)lista).Add(CriarVeiculo(2, "XYZ9K87")));
        Assert.Equal(1, frota.Quantidade);
    }

    [Fact]
    public void MesmoVeiculo_PodeEstarEmDuasFrotas()
    {
        var centro = new Frota("Centro");
        var aeroporto = new Frota("Aeroporto");
        var veiculo = CriarVeiculo(1, "ABC1D23");

        centro.Adicionar(veiculo);
        aeroporto.Adicionar(veiculo);

        Assert.Same(centro.Listar()[0], aeroporto.Listar()[0]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Construtor_ComNomeVazio_LancaExcecao(string? nome)
    {
        Assert.Throws<ArgumentException>(() => new Frota(nome!));
    }
}
