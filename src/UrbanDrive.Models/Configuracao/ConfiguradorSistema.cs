namespace UrbanDrive.Models.Configuracao;

// Membros estáticos: existe um único valor de taxa e de contador para todo o sistema.
public static class ConfiguradorSistema
{
    private static decimal _taxaServico = 0.10m;
    private static int _totalVeiculos;

    // Percentual aplicado sobre o valor das locações (0,10 = 10%).
    public static decimal TaxaServico
    {
        get => _taxaServico;
        set
        {
            if (value < 0)
                throw new ArgumentException("A taxa de serviço não pode ser negativa.");

            _taxaServico = value;
        }
    }

    public static int TotalVeiculos => _totalVeiculos;

    // internal: só as classes do projeto Models (o construtor de Veiculo) podem incrementar.
    internal static void RegistrarVeiculoCriado() => _totalVeiculos++;
}
