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
var relatorioService = new RelatorioService(veiculoService, motoristaService, frotaService, locacaoService);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== UrbanDrive ===");
    Console.WriteLine("Veículos");
    Console.WriteLine("  1 - Cadastrar veículo");
    Console.WriteLine("  2 - Listar veículos");
    Console.WriteLine("  3 - Simular aluguel de todos os veículos");
    Console.WriteLine("  4 - Registrar manutenção");
    Console.WriteLine("  5 - Recarregar veículo elétrico");
    Console.WriteLine("Motoristas");
    Console.WriteLine("  6 - Cadastrar motorista");
    Console.WriteLine("  7 - Listar motoristas");
    Console.WriteLine("Frota");
    Console.WriteLine("  8 - Adicionar veículo à frota");
    Console.WriteLine("  9 - Remover veículo da frota");
    Console.WriteLine("  10 - Listar frota");
    Console.WriteLine("Locações");
    Console.WriteLine("  11 - Realizar locação");
    Console.WriteLine("  12 - Listar locações");
    Console.WriteLine("Relatórios e configurações");
    Console.WriteLine("  13 - Gerar relatórios");
    Console.WriteLine("  14 - Ver configurações");
    Console.WriteLine("  15 - Alterar taxa de serviço");
    Console.WriteLine("  0 - Sair");
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
            SimularAluguel();
            break;
        case "4":
            RegistrarManutencao();
            break;
        case "5":
            RecarregarVeiculo();
            break;
        case "6":
            CadastrarMotorista();
            break;
        case "7":
            ListarMotoristas();
            break;
        case "8":
            AdicionarVeiculoNaFrota();
            break;
        case "9":
            RemoverVeiculoDaFrota();
            break;
        case "10":
            ListarFrota();
            break;
        case "11":
            RealizarLocacao();
            break;
        case "12":
            ListarLocacoes();
            break;
        case "13":
            GerarRelatorios();
            break;
        case "14":
            VerConfiguracoes();
            break;
        case "15":
            AlterarTaxa();
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
    Console.WriteLine("Tipo: 1 - Carro | 2 - Carro elétrico | 3 - Patinete");
    Console.Write("Escolha o tipo: ");
    var tipo = Console.ReadLine();
    if (tipo is not ("1" or "2" or "3"))
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
    if (tipo is "1" or "2" && !LerInteiro("Quantidade de portas: ", out portas))
        return;
    if (tipo == "3" && !LerInteiro("Autonomia da bateria (km): ", out autonomia))
        return;

    Executar(() =>
    {
        Veiculo veiculo = tipo switch
        {
            "1" => veiculoService.CadastrarCarro(placa, modelo, valorDiaria, portas),
            "2" => veiculoService.CadastrarCarroEletrico(placa, modelo, valorDiaria, portas),
            _ => veiculoService.CadastrarPatinete(placa, modelo, valorDiaria, autonomia),
        };
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

void RecarregarVeiculo()
{
    if (!LerInteiro("Id do veículo elétrico: ", out var id))
        return;

    Executar(() =>
    {
        var eletrico = veiculoService.Recarregar(id);
        Console.WriteLine($"Veículo recarregado. Bateria: {eletrico.NivelBateria}%");
    });
}

// Cada entidade gera o próprio relatório pelo contrato IImprimivel.
void GerarRelatorios()
{
    foreach (var relatorio in relatorioService.GerarTodos())
    {
        Console.WriteLine();
        Console.WriteLine(relatorio);
    }
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
