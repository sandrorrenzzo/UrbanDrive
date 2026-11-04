# UrbanDrive

Sistema de gestão de mobilidade urbana em C#, com aplicação de console. Permite cadastrar carros, carros elétricos e patinetes, motoristas com CNH, montar uma frota, realizar locações com cálculo de valor por tipo de veículo, registrar manutenções, recarregar veículos elétricos e gerar relatórios.

Trabalho prático da disciplina de Programação Orientada a Objetos, desenvolvido em quatro sprints.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0) ou superior. Para conferir a versão instalada:

  ```bash
  dotnet --version
  ```

- [Git](https://git-scm.com/), para clonar o repositório.

## Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/sandrorrenzzo/UrbanDrive.git
   ```

2. Entre na pasta do projeto:

   ```bash
   cd UrbanDrive
   ```

3. Restaure as dependências e compile:

   ```bash
   dotnet build
   ```

   O build deve terminar com `0 Erro(s)`.

## Execução

Para abrir o menu do sistema:

```bash
dotnet run --project src/UrbanDrive.App
```

O menu é organizado em seções:

- **Veículos:** cadastrar (carro, carro elétrico ou patinete), listar, simular o aluguel de todos os veículos, registrar manutenção e recarregar veículos elétricos.
- **Motoristas:** cadastrar e listar.
- **Frota:** adicionar, remover e listar veículos.
- **Locações:** realizar e listar.
- **Relatórios e configurações:** gerar os relatórios de todas as entidades, ver e alterar a taxa de serviço.

Os dados ficam em memória enquanto o programa está aberto.

### Exemplo rápido

1. Opção `1`, tipo `1` (carro): placa `ABC1D23`, modelo `Onix`, diária `150`, `4` portas.
2. Opção `1`, tipo `3` (patinete): placa `PAT0001`, modelo `Xiaomi M365`, diária `30`, autonomia `25`.
3. Opção `3` com `3` dias: o carro custa R$ 555,00 e o patinete R$ 79,20.
4. Opção `6`: cadastre um motorista com CPF de 11 dígitos e CNH com validade futura (formato `dd/mm/aaaa`).
5. Opção `11`: informe o CPF, o Id do veículo e os dias para registrar a locação.
6. Opção `13`: veja os relatórios de tudo o que foi cadastrado.

## Testes

Para rodar todos os testes automatizados (xUnit):

```bash
dotnet test
```

O resultado esperado é `Aprovado!` com 118 testes e nenhuma falha.

## Estrutura do projeto

```text
UrbanDrive/
  src/
    UrbanDrive.Models/      entidades, enums, interfaces e ConfiguradorSistema
    UrbanDrive.Services/    serviços com as operações usadas pelo menu
    UrbanDrive.App/         aplicação de console (Program.cs)
  tests/
    UrbanDrive.Tests/       testes xUnit, nas mesmas pastas do src
  docs/
    backlog.md              Product Backlog e histórico de alterações
    sprints/                planejamento, resultado e retrospectiva de cada sprint, e evidências
    diagramas/              diagramas de classes de cada sprint e o final
    relatorio-final.md      evolução das sprints, decisões e dificuldades
    roteiro-apresentacao.md roteiro da demonstração
```

## Documentação

- [Product Backlog](docs/backlog.md)
- Sprints: [1](docs/sprints/sprint-1.md), [2](docs/sprints/sprint-2.md), [3](docs/sprints/sprint-3.md) e [4](docs/sprints/sprint-4.md)
- [Diagrama de classes final](docs/diagramas/classes-final.md)
- [Relatório final](docs/relatorio-final.md)
- [Roteiro de apresentação](docs/roteiro-apresentacao.md)
- [Quadro do projeto no GitHub](https://github.com/users/sandrorrenzzo/projects/1)

## Autor

Sandro Renzzo
