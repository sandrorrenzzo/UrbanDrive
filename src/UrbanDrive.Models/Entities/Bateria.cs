namespace UrbanDrive.Models.Entities;

// Patinete e CarroEletrico têm classes-base diferentes (Veiculo e Carro), então não podem herdar
// a lógica de bateria de um lugar comum. Cada um tem uma Bateria por composição, e a regra fica só aqui.
internal sealed class Bateria
{
    public const int ConsumoPorDia = 10;
    public const int NivelMinimoParaLocacao = 20;

    private int _nivel = 100;

    public int Nivel => _nivel;

    public void ConsumirParaLocacao(int dias)
    {
        if (_nivel < NivelMinimoParaLocacao)
            throw new InvalidOperationException(
                $"Bateria em {_nivel}%. Recarregue o veículo antes de locar (mínimo de {NivelMinimoParaLocacao}%).");

        _nivel = Math.Max(0, _nivel - (ConsumoPorDia * dias));
    }

    public void Recarregar() => _nivel = 100;
}
