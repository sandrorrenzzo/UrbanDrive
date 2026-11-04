namespace UrbanDrive.Models.Interfaces;

// Contrato dos veículos elétricos: têm bateria, gastam bateria nas locações e podem ser recarregados.
public interface IEletrico
{
    // Nível da bateria em %, de 0 a 100.
    int NivelBateria { get; }

    // Desconta da bateria o consumo de uma locação; recusa se a bateria estiver baixa demais.
    void ConsumirBateria(int dias);

    // Volta a bateria para 100%.
    void Recarregar();
}
