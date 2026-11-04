# Roteiro de apresentação

Duração sugerida: 10 a 15 minutos. Para cada conceito, mostre primeiro o código e depois o comportamento no programa.

## Preparação

1. Abra o projeto no VS Code.
2. Deixe dois terminais abertos na pasta do projeto.
3. No primeiro, rode os testes:

   ```bash
   dotnet test
   ```

   Resultado esperado: 118 testes aprovados.
4. No segundo, abra o menu:

   ```bash
   dotnet run --project src/UrbanDrive.App
   ```

## 1. Organização do trabalho (2 min)

- **[Product Backlog](backlog.md):** 15 itens priorizados, com o histórico de alterações no fim.
- **[Quadro do GitHub Projects](https://github.com/users/sandrorrenzzo/projects/1):** as 15 issues em Done.
- **Pull requests:** um por item, com `Closes #N`.
- **Um planejamento de sprint**, por exemplo o [sprint-3.md](sprints/sprint-3.md): objetivo, tarefas com horas, critérios de aceitação, resultado e retrospectiva.

## 2. Encapsulamento (1 min)

- **Código:** `src/UrbanDrive.Models/Entities/Veiculo.cs`: campo `private string _placa` e a propriedade `Placa` que valida no `set`; `Id` só com `get` e o campo `readonly`.
- **Programa:** opção `1`, tipo `1`, com a placa vazia, mostra "A placa não pode ser nula ou vazia.".

## 3. Composição, agregação e associação (2 min)

- **Composição:** `Motorista.cs` cria a `CNH` no construtor; em `CNH.cs`, o construtor é `internal`.
- **Agregação:** `Frota.cs` guarda uma lista de veículos que já existem.
- **Associação:** `Locacao.cs` recebe um motorista e um veículo e só os usa.
- **Programa:**
  1. Opção `1` duas vezes: cadastre um carro (`ABC1D23`, `Onix`, `150`, `4` portas) e um patinete (tipo `3`, `PAT0001`, `Xiaomi M365`, `30`, `25` km).
  2. Opção `6`: cadastre um motorista (CPF `12345678909`, CNH `12345678900`, categoria `B`, validade `15/03/2028`).
  3. Opção `8` com os Ids `1` e `2`, e depois a opção `10`, para listar a frota.
  4. Opção `9` com o Id `2`, para remover o patinete da frota; a opção `2` mostra que ele continua cadastrado.

## 4. Membros estáticos (1 min)

- **Código:** `src/UrbanDrive.Models/Configuracao/ConfiguradorSistema.cs`.
- **Programa:** opção `14` mostra a taxa e o total de veículos criados. A opção `15`, com `20`, altera a taxa para todo o sistema.
- Volte a taxa para `10` antes de seguir.

## 5. Herança, sobrescrita e polimorfismo (2 min)

- **Código:**
  - `Carro.cs` e `Patinete.cs`: o `: Veiculo` e o `: base(...)` no construtor.
  - `override CalcularAluguel` chamando `base.CalcularAluguel(dias)`.
  - `Patinete` é `sealed`.
- **Programa:** opção `3` com `3` dias calcula cada veículo com a sua regra (carro R$ 555,00, patinete R$ 79,20), com a mesma chamada.

## 6. Sobrecarga (1 min)

- **Código:** as duas versões de `RealizarManutencao` em `Veiculo.cs`.
- **Programa:** opção `4` no veículo `1`, uma vez com a peça vazia e outra com `pneu`, mostra o histórico com as duas.

## 7. Abstração (1 min)

- **Código:** `abstract class Veiculo`, construtor `protected` e `abstract string Tipo`, que cada classe filha define.
- **Ponto para comentar:** `new Veiculo(...)` não compila; o menu só cadastra tipos concretos.

## 8. Interfaces (2 min)

- **Código:** `src/UrbanDrive.Models/Interfaces/IImprimivel.cs` e `IEletrico.cs`; `CarroEletrico : Carro, IEletrico`.
- **Programa:**
  1. Opção `1`, tipo `2`: cadastre um carro elétrico (`ELE1C23`, `Dolphin`, `150`, `4` portas).
  2. Opção `11`: loque o patinete (Id `2`) por `9` dias. A bateria vai para 10%.
  3. Opção `11` de novo com o patinete: a locação é recusada por bateria baixa.
  4. Opção `5` com o Id `2`: recarrega para 100%.
  5. Opção `5` com o Id `1`: recusada, porque o carro comum não é elétrico.
  6. Opção `13`: gera os relatórios de veículos, motorista, frota e locações pelo mesmo contrato.

## 9. Testes e encerramento (1 min)

- **Testes:** volte ao terminal do `dotnet test` (118 aprovados) e mostre a pasta `tests/UrbanDrive.Tests`, que espelha o `src`.
- **Documentação final:** [diagrama de classes final](diagramas/classes-final.md) e [relatório final](relatorio-final.md), com as decisões e as dificuldades.

## Perguntas prováveis

- **Por que a CNH não pode ser criada fora do Motorista?** Composição: no modelo, a CNH não existe sem o motorista.
- **Qual a diferença entre sobrecarga e sobrescrita?** Sobrecarga é o mesmo nome com parâmetros diferentes, escolhido na compilação. Sobrescrita é o mesmo método redefinido na classe filha, escolhido na execução pelo tipo real do objeto.
- **Por que `Veiculo` é abstrata e `IImprimivel` é interface?** A classe abstrata tem código compartilhado pela hierarquia; a interface é só um contrato, que classes sem relação de herança podem implementar.
- **Por que existe a classe `Bateria`?** `Patinete` e `CarroEletrico` têm bases diferentes, então a regra de bateria fica numa classe usada por composição, sem repetir código.
- **Por que os testes não rodam em paralelo?** O `ConfiguradorSistema` é estático; em paralelo, um teste alteraria a taxa ou o contador que outro está conferindo.
