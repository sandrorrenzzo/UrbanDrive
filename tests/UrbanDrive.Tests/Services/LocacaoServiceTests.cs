using UrbanDrive.Models.Enums;
using UrbanDrive.Services;

namespace UrbanDrive.Tests.Services;

public class LocacaoServiceTests
{
    private const string Cpf = "12345678909";

    private static (LocacaoService Locacoes, VeiculoService Veiculos) CriarServicos()
    {
        var motoristas = new MotoristaService();
        var veiculos = new VeiculoService();
        motoristas.Cadastrar("Ana Souza", Cpf, "12345678900", CategoriaCnh.B, DateOnly.FromDateTime(DateTime.Today.AddYears(2)));

        return (new LocacaoService(motoristas, veiculos), veiculos);
    }

    [Fact]
    public void Registrar_ComMotoristaEVeiculoCadastrados_AdicionaNaLista()
    {
        var (locacoes, veiculos) = CriarServicos();
        var veiculo = veiculos.CadastrarCarro("ABC1D23", "Onix", 150m, 4);

        var locacao = locacoes.Registrar("123.456.789-09", veiculo.Id, 3);

        Assert.Same(veiculo, locacao.Veiculo);
        Assert.Equal(Cpf, locacao.Motorista.Cpf);
        Assert.Single(locacoes.Listar());
    }

    [Fact]
    public void Registrar_ComCpfNaoCadastrado_LancaExcecao()
    {
        var (locacoes, veiculos) = CriarServicos();
        var veiculo = veiculos.CadastrarCarro("ABC1D23", "Onix", 150m, 4);

        var erro = Assert.Throws<ArgumentException>(() => locacoes.Registrar("98765432100", veiculo.Id, 3));

        Assert.Equal("Motorista não encontrado.", erro.Message);
        Assert.Empty(locacoes.Listar());
    }

    [Fact]
    public void Registrar_ComVeiculoNaoCadastrado_LancaExcecao()
    {
        var (locacoes, _) = CriarServicos();

        var erro = Assert.Throws<ArgumentException>(() => locacoes.Registrar(Cpf, 9999, 3));

        Assert.Equal("Veículo não encontrado.", erro.Message);
        Assert.Empty(locacoes.Listar());
    }

    [Fact]
    public void Registrar_ComDiasInvalidos_NaoAdicionaNaLista()
    {
        var (locacoes, veiculos) = CriarServicos();
        var veiculo = veiculos.CadastrarCarro("ABC1D23", "Onix", 150m, 4);

        Assert.Throws<ArgumentException>(() => locacoes.Registrar(Cpf, veiculo.Id, 0));
        Assert.Empty(locacoes.Listar());
    }
}
