using System.Globalization;
using UrbanDrive.Models.Configuracao;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;
using UrbanDrive.Services;

CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

var veiculoService = new VeiculoService();
var motoristaService = new MotoristaService();
var locacaoService = new LocacaoService(motoristaService, veiculoService);
var frotaService = new FrotaService(new Frota("Principal"), veiculoService);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== UrbanDrive ===");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Listar veículos");
    Console.WriteLine("3 - Cadastrar motorista");
    Console.WriteLine("4 - Listar motoristas");
    Console.WriteLine("5 - Adicionar veículo à frota");
    Console.WriteLine("6 - Remover veículo da frota");
    Console.WriteLine("7 - Listar frota");
    Console.WriteLine("8 - Realizar locação");
    Console.WriteLine("9 - Listar locações");
    Console.WriteLine("10 - Ver configurações");
    Console.WriteLine("11 - Alterar taxa de serviço");
    Console.WriteLine("12 - Simular aluguel de todos os veículos");
    Console.WriteLine("13 - Registrar manutenção");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarVeiculo();
            break;
        case "2":
            ListarVeiculos();
            break;
        case "3":
            CadastrarMotorista();
            break;
        case "4":
            ListarMotoristas();
            break;
        case "5":
            AdicionarVeiculoNaFrota();
            break;
        case "6":
            RemoverVeiculoDaFrota();
            break;
        case "7":
            ListarFrota();
            break;
        case "8":
            RealizarLocacao();
            break;
        case "9":
            ListarLocacoes();
            break;
        case "10":
            VerConfiguracoes();
            break;
        case "11":
            AlterarTaxa();
            break;
        case "12":
            SimularAluguel();
            break;
        case "13":
            RegistrarManutencao();
            break;
        case "0":
        case null:
            return;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

void CadastrarVeiculo()
{
    Console.WriteLine("Tipo: 1 - Carro | 2 - Patinete");
    Console.Write("Escolha o tipo: ");
    var tipo = Console.ReadLine();
    if (tipo is not ("1" or "2"))
    {
        Console.WriteLine("Erro: tipo inválido.");
        return;
    }

    Console.Write("Placa: ");
    var placa = Console.ReadLine() ?? string.Empty;

    Console.Write("Modelo: ");
    var modelo = Console.ReadLine() ?? string.Empty;

    Console.Write("Valor da diária: ");
    if (!decimal.TryParse(Console.ReadLine(), out var valorDiaria))
    {
        Console.WriteLine("Erro: o valor da diária deve ser um número.");
        return;
    }

    var portas = 0;
    var autonomia = 0;
    if (tipo == "1" && !LerInteiro("Quantidade de portas: ", out portas))
        return;
    if (tipo == "2" && !LerInteiro("Autonomia da bateria (km): ", out autonomia))
        return;

    Executar(() =>
    {
        Veiculo veiculo = tipo == "1"
            ? veiculoService.CadastrarCarro(placa, modelo, valorDiaria, portas)
            : veiculoService.CadastrarPatinete(placa, modelo, valorDiaria, autonomia);
        Console.WriteLine($"Veículo cadastrado: {veiculo}");
    });
}

void ListarVeiculos() => Exibir(veiculoService.Listar(), "Nenhum veículo cadastrado.");

void CadastrarMotorista()
{
    Console.Write("Nome: ");
    var nome = Console.ReadLine() ?? string.Empty;

    Console.Write("CPF: ");
    var cpf = Console.ReadLine() ?? string.Empty;

    Console.Write("Número da CNH: ");
    var numeroCnh = Console.ReadLine() ?? string.Empty;

    Console.Write("Categoria da CNH (A, B, AB, C, D, E): ");
    if (!Enum.TryParse<CategoriaCnh>(Console.ReadLine(), true, out var categoria))
    {
        Console.WriteLine("Erro: categoria inválida. Use A, B, AB, C, D ou E.");
        return;
    }

    Console.Write("Validade da CNH (dd/mm/aaaa): ");
    if (!DateOnly.TryParse(Console.ReadLine(), out var validade))
    {
        Console.WriteLine("Erro: data inválida. Use o formato dd/mm/aaaa.");
        return;
    }

    Executar(() =>
    {
        var motorista = motoristaService.Cadastrar(nome, cpf, numeroCnh, categoria, validade);
        Console.WriteLine($"Motorista cadastrado: {motorista}");
    });
}

void ListarMotoristas() => Exibir(motoristaService.Listar(), "Nenhum motorista cadastrado.");

void AdicionarVeiculoNaFrota()
{
    if (!LerInteiro("Id do veículo: ", out var id))
        return;

    Executar(() =>
    {
        var veiculo = frotaService.Adicionar(id);
        Console.WriteLine($"Veículo {veiculo.Placa} adicionado. {frotaService.Frota}");
    });
}

void RemoverVeiculoDaFrota()
{
    if (!LerInteiro("Id do veículo: ", out var id))
        return;

    Executar(() =>
    {
        var veiculo = frotaService.Remover(id);
        Console.WriteLine($"Veículo {veiculo.Placa} removido. {frotaService.Frota}");
    });
}

void ListarFrota()
{
    Console.WriteLine(frotaService.Frota);
    Exibir(frotaService.Listar(), "A frota está vazia.");
}

void RealizarLocacao()
{
    Console.Write("CPF do motorista: ");
    var cpf = Console.ReadLine() ?? string.Empty;

    if (!LerInteiro("Id do veículo: ", out var idVeiculo) || !LerInteiro("Quantidade de dias: ", out var dias))
        return;

    Executar(() =>
    {
        var locacao = locacaoService.Registrar(cpf, idVeiculo, dias);
        Console.WriteLine($"Locação registrada: {locacao}");
    });
}

void ListarLocacoes() => Exibir(locacaoService.Listar(), "Nenhuma locação registrada.");

void VerConfiguracoes()
{
    Console.WriteLine($"Taxa de serviço: {ConfiguradorSistema.TaxaServico:P0}");
    Console.WriteLine($"Total de veículos criados: {ConfiguradorSistema.TotalVeiculos}");
}

void AlterarTaxa()
{
    Console.Write("Nova taxa em % (ex.: 15): ");
    if (!decimal.TryParse(Console.ReadLine(), out var percentual))
    {
        Console.WriteLine("Erro: a taxa deve ser um número.");
        return;
    }

    Executar(() =>
    {
        ConfiguradorSistema.TaxaServico = percentual / 100;
        Console.WriteLine($"Taxa de serviço alterada para {ConfiguradorSistema.TaxaServico:P0}.");
    });
}

// Polimorfismo: a mesma chamada CalcularAluguel usa a regra do tipo real de cada veículo.
void SimularAluguel()
{
    if (!LerInteiro("Quantidade de dias: ", out var dias))
        return;

    var veiculos = veiculoService.Listar();
    if (veiculos.Count == 0)
    {
        Console.WriteLine("Nenhum veículo cadastrado.");
        return;
    }

    Executar(() =>
    {
        foreach (var veiculo in veiculos)
            Console.WriteLine($"{veiculo} => {dias} dia(s): {veiculo.CalcularAluguel(dias):C}");
    });
}

void RegistrarManutencao()
{
    if (!LerInteiro("Id do veículo: ", out var id))
        return;

    Console.Write("Peça substituída (deixe vazio para manutenção geral): ");
    var peca = Console.ReadLine();

    Executar(() =>
    {
        var registro = string.IsNullOrWhiteSpace(peca)
            ? veiculoService.RealizarManutencao(id)
            : veiculoService.RealizarManutencao(id, peca);
        Console.WriteLine($"Manutenção registrada: {registro}");

        Console.WriteLine("Histórico do veículo:");
        foreach (var item in veiculoService.BuscarPorId(id)!.HistoricoManutencoes)
            Console.WriteLine($"  {item}");
    });
}

// Lê um número inteiro do console; mostra erro e devolve false se o texto não for um número.
bool LerInteiro(string rotulo, out int valor)
{
    Console.Write(rotulo);
    if (int.TryParse(Console.ReadLine(), out valor))
        return true;

    Console.WriteLine("Erro: informe um número inteiro.");
    return false;
}

// Mostra cada item da lista, ou a mensagem quando a lista está vazia.
void Exibir<T>(IReadOnlyList<T> itens, string mensagemVazia)
{
    if (itens.Count == 0)
    {
        Console.WriteLine(mensagemVazia);
        return;
    }

    foreach (var item in itens)
        Console.WriteLine(item);
}

// Executa a ação e mostra a mensagem das regras de negócio quando algo é recusado.
void Executar(Action acao)
{
    try
    {
        acao();
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}
