using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Services;

public class VeiculoService
{
    private readonly List<Veiculo> _veiculos = new();

    // O próximo Id vem do contador compartilhado do sistema.
    public Carro CadastrarCarro(string placa, string modelo, decimal valorDiaria, int quantidadePortas) =>
        Adicionar(new Carro(ProximoId(), placa, modelo, valorDiaria, quantidadePortas));

    public CarroEletrico CadastrarCarroEletrico(string placa, string modelo, decimal valorDiaria, int quantidadePortas) =>
        Adicionar(new CarroEletrico(ProximoId(), placa, modelo, valorDiaria, quantidadePortas));

    public Patinete CadastrarPatinete(string placa, string modelo, decimal valorDiaria, int autonomiaKm) =>
        Adicionar(new Patinete(ProximoId(), placa, modelo, valorDiaria, autonomiaKm));

    public IReadOnlyList<Veiculo> Listar() => _veiculos.AsReadOnly();

    public Veiculo? BuscarPorId(int id) => _veiculos.FirstOrDefault(v => v.Id == id);

    public string RealizarManutencao(int idVeiculo) => ObterPorId(idVeiculo).RealizarManutencao();

    public string RealizarManutencao(int idVeiculo, string pecaSubstituida) =>
        ObterPorId(idVeiculo).RealizarManutencao(pecaSubstituida);

    // Só veículos que assinam o contrato IEletrico podem ser recarregados.
    public IEletrico Recarregar(int idVeiculo)
    {
        if (ObterPorId(idVeiculo) is not IEletrico eletrico)
            throw new ArgumentException("Este veículo não é elétrico.");

        eletrico.Recarregar();
        return eletrico;
    }

    private static int ProximoId() => ConfiguradorSistema.TotalVeiculos + 1;

    private T Adicionar<T>(T veiculo) where T : Veiculo
    {
        _veiculos.Add(veiculo);
        return veiculo;
    }

    private Veiculo ObterPorId(int id) =>
        BuscarPorId(id) ?? throw new ArgumentException("Veículo não encontrado.");
}
