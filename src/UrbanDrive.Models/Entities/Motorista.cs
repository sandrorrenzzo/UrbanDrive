using UrbanDrive.Models.Enums;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities;

public class Motorista : IImprimivel
{
    private string _nome = string.Empty;
    private readonly string _cpf;
    private readonly CNH _cnh;

    public Motorista(string nome, string cpf, string numeroCnh, CategoriaCnh categoriaCnh, DateOnly validadeCnh)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("O CPF não pode ser nulo ou vazio.");

        var cpfSomenteDigitos = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpfSomenteDigitos.Length != 11)
            throw new ArgumentException("O CPF deve ter 11 dígitos.");

        Nome = nome;
        _cpf = cpfSomenteDigitos;

        // Composição: a CNH é criada aqui dentro e pertence ao motorista.
        _cnh = new CNH(numeroCnh, categoriaCnh, validadeCnh);
    }

    public string Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("O nome não pode ser nulo ou vazio.");

            _nome = value.Trim();
        }
    }

    public string Cpf => _cpf;

    public CNH Cnh => _cnh;

    // A CNH entra no relatório do motorista porque só existe dentro dele (composição).
    public string GerarRelatorio() => string.Join(Environment.NewLine,
        "=== Relatório do motorista ===",
        $"Nome: {Nome}",
        $"CPF: {Cpf}",
        $"CNH: {Cnh.Numero} | Categoria {Cnh.Categoria} | Validade {Cnh.Validade:dd/MM/yyyy}");

    public override string ToString() => $"{Nome} | CPF {Cpf} | {Cnh}";
}
