using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Enums;

namespace UrbanDrive.Services;

public class MotoristaService
{
    private readonly List<Motorista> _motoristas = new();

    public Motorista Cadastrar(string nome, string cpf, string numeroCnh, CategoriaCnh categoriaCnh, DateOnly validadeCnh)
    {
        var motorista = new Motorista(nome, cpf, numeroCnh, categoriaCnh, validadeCnh);
        _motoristas.Add(motorista);
        return motorista;
    }

    public IReadOnlyList<Motorista> Listar() => _motoristas.AsReadOnly();

    public Motorista? BuscarPorCpf(string cpf)
    {
        var cpfSomenteDigitos = new string(cpf.Where(char.IsDigit).ToArray());
        return _motoristas.FirstOrDefault(m => m.Cpf == cpfSomenteDigitos);
    }
}
