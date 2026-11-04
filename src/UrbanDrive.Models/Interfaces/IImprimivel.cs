namespace UrbanDrive.Models.Interfaces;

// Contrato comum: toda entidade que implementa sabe gerar um relatório em texto sobre si mesma.
public interface IImprimivel
{
    string GerarRelatorio();
}
