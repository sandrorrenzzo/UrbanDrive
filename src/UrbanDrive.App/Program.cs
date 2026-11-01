using System.Globalization;
using UrbanDrive.Services;

CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

var veiculoService = new VeiculoService();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== UrbanDrive ===");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Listar veículos");
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

    try
    {
        var veiculo = veiculoService.Cadastrar(placa, modelo, valorDiaria);
        Console.WriteLine($"Veículo cadastrado: {veiculo}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void ListarVeiculos()
{
    var veiculos = veiculoService.Listar();

    if (veiculos.Count == 0)
    {
        Console.WriteLine("Nenhum veículo cadastrado.");
        return;
    }

    foreach (var veiculo in veiculos)
        Console.WriteLine(veiculo);
}
