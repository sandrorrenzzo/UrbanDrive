# Sprint 3: Herança, polimorfismo e especialização

## A. Identificação

- **Grupo:** individual
- **Integrantes:** Sandro Renzzo
- **Número da sprint:** 3
- **Período:** 03/11/2026 a 09/11/2026
- **Responsável pelo acompanhamento:** Sandro Renzzo

> A Sprint 2 estava prevista até 09/11/2026, mas foi concluída em 03/11/2026. Por isso a Sprint 3 começa no mesmo dia e usa o restante desse período.

## B. Objetivo da sprint

Ter carros e patinetes no UrbanDrive, cada um com suas características e sua própria regra de aluguel, reaproveitando o que já existe em `Veiculo`. Ao final, uma mesma lista de veículos deve calcular valores diferentes conforme o tipo de cada um, as locações devem usar esse cálculo, e deve ser possível registrar manutenções com ou sem peça substituída.

## C. Itens selecionados do Product Backlog

**PB08**
- **ID:** PB08 (issue #8)
- **Requisito ou item:** RF02, `Carro` e `Patinete` (`sealed`) herdando de `Veiculo`
- **Prioridade:** Alta
- **Justificativa:** é a base da sprint: sem as classes filhas não há o que sobrescrever. Depende só de `Veiculo`, que já existe.

**PB09**
- **ID:** PB09 (issue #9)
- **Requisito ou item:** RF05, `CalcularAluguel(int dias)` com cálculo por tipo
- **Prioridade:** Alta
- **Justificativa:** é onde aparecem a sobrescrita e o polimorfismo. Depende de PB08 e da taxa do `ConfiguradorSistema` (PB06, concluído). Também resolve a pendência da Sprint 2: o `ValorTotal` da `Locacao` passa a usar esse método.

**PB10**
- **ID:** PB10 (issue #10)
- **Requisito ou item:** RF06, `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)`
- **Prioridade:** Média
- **Justificativa:** cobre a sobrecarga pedida na sprint. É independente dos outros itens e tem esforço pequeno.

**Regras de aluguel**
- **Comum a todos (em `Veiculo`):** diária × dias, acrescido da taxa do `ConfiguradorSistema`. Dias menor ou igual a zero é recusado.
- **`Carro`:** valor comum + seguro de R$ 20,00 por dia.
- **`Patinete`:** valor comum com 20% de desconto.

**Como a sprint evita duplicação de código**
- O cálculo comum fica uma única vez em `Veiculo.CalcularAluguel`, marcado como `virtual`.
- `Carro` e `Patinete` usam `override` e chamam `base.CalcularAluguel(dias)`, acrescentando só a sua diferença (o seguro ou o desconto). A validação dos dias e a taxa não são repetidas.
- Os construtores de `Carro` e `Patinete` usam `: base(...)`, então as validações de Id, placa, modelo e diária e o contador de veículos continuam só em `Veiculo`.
- O histórico de manutenção fica em `Veiculo` e vale para todos os tipos.

**Como a sprint vai demonstrar o polimorfismo**
- Um teste monta uma `List<Veiculo>` com um carro e um patinete e confere que `CalcularAluguel` devolve o valor de cada tipo, sem `if` por tipo.
- A `Locacao` continua recebendo um `Veiculo` e chama `CalcularAluguel`; o valor muda conforme o objeto real ser um carro ou um patinete.
- O menu terá uma opção que calcula o aluguel de todos os veículos cadastrados para a mesma quantidade de dias.

## D. Planejamento das tarefas

Unidade utilizada nas estimativas: horas.

Seguindo a retrospectiva da Sprint 2, o planejamento já inclui as mudanças nos serviços que o menu vai precisar, e a seção G terá o tempo real de cada tarefa.

**T01**
- **ID:** T01
- **Tarefa:** criar `Carro : Veiculo` com quantidade de portas e construtor usando `: base(...)`
- **Item relacionado:** PB08
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T02**
- **ID:** T02
- **Tarefa:** criar `sealed Patinete : Veiculo` com autonomia da bateria e construtor usando `: base(...)`
- **Item relacionado:** PB08
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T03**
- **ID:** T03
- **Tarefa:** escrever os testes de criação e validação de `Carro` e `Patinete`
- **Item relacionado:** PB08
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T04**
- **ID:** T04
- **Tarefa:** criar `CalcularAluguel(int dias)` como `virtual` em `Veiculo`, com a regra comum
- **Item relacionado:** PB09
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T05**
- **ID:** T05
- **Tarefa:** sobrescrever `CalcularAluguel` em `Carro` (seguro) e `Patinete` (desconto) usando `base.CalcularAluguel`
- **Item relacionado:** PB09
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T06**
- **ID:** T06
- **Tarefa:** fazer o `ValorTotal` da `Locacao` usar `CalcularAluguel` e escrever os testes de cálculo, incluindo a lista mista de veículos
- **Item relacionado:** PB09
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T07**
- **ID:** T07
- **Tarefa:** criar `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)` em `Veiculo`, com histórico, e os testes das duas sobrecargas
- **Item relacionado:** PB10
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T08**
- **ID:** T08
- **Tarefa:** adicionar ao `VeiculoService` o cadastro de carro e de patinete, com testes
- **Item relacionado:** PB08
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T09**
- **ID:** T09
- **Tarefa:** adaptar o menu para cadastrar carro ou patinete, simular o aluguel de todos os veículos e registrar manutenção
- **Item relacionado:** PB08, PB09, PB10
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 1 h

**T10**
- **ID:** T10
- **Tarefa:** rodar o programa e os testes e salvar as evidências
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T11**
- **ID:** T11
- **Tarefa:** atualizar o diagrama de classes com a hierarquia de veículos
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T12**
- **ID:** T12
- **Tarefa:** atualizar o backlog e registrar o resultado e a retrospectiva da sprint
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**Total:** 6,5 h (3,5 h dos itens do backlog, 1,5 h de serviço e menu e 1,5 h de encerramento)

## E. Critérios de aceitação

**PB08: `Carro` e `Patinete`**
- `Carro` e `Patinete` herdam de `Veiculo` e têm Id, placa, modelo e diária sem redeclarar esses campos.
- Os construtores usam `: base(...)`; placa vazia ou diária inválida continuam sendo recusadas, e criar um carro ou patinete incrementa o contador de veículos.
- `Carro` tem quantidade de portas maior que zero; `Patinete` tem autonomia da bateria maior que zero. Valores inválidos lançam exceção.
- `Patinete` é `sealed`: nenhuma classe consegue herdar dele.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB09: `CalcularAluguel(int dias)`**
- Em `Veiculo`, o método é `virtual` e calcula diária × dias × (1 + taxa).
- `Carro` e `Patinete` usam `override` e reaproveitam o cálculo comum com `base.CalcularAluguel(dias)`.
- Com taxa de 10%: um carro de R$ 150,00 por 3 dias custa R$ 555,00 e um patinete de R$ 30,00 por 3 dias custa R$ 79,20.
- Dias menor ou igual a zero lança exceção em todos os tipos.
- Uma `List<Veiculo>` com carro e patinete devolve o valor certo de cada um sem verificar o tipo.
- O `ValorTotal` da `Locacao` usa `CalcularAluguel`, então uma locação de carro inclui o seguro e uma de patinete inclui o desconto.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB10: Manutenção**
- `RealizarManutencao()` registra uma manutenção geral no histórico do veículo.
- `RealizarManutencao(string pecaSubstituida)` registra a peça substituída no histórico.
- Peça vazia lança exceção.
- O histórico pode ser consultado mas não alterado de fora da classe.
- Existe pelo menos um teste para cada sobrecarga e todos passam.

**Menu**
- É possível cadastrar um carro ou um patinete com suas características próprias.
- Uma opção mostra o valor do aluguel de todos os veículos para a mesma quantidade de dias.
- Uma opção registra manutenção com ou sem peça e mostra o histórico do veículo.
- O `Program.cs` continua sem regra de negócio.

## F. Entregáveis previstos

- Código com `Carro`, `Patinete`, `CalcularAluguel` e as duas sobrecargas de manutenção, integrado na `master` por pull requests que fecham as issues #8, #9 e #10 com `Closes #N`.
- Testes dos comportamentos especializados, incluindo a lista mista de veículos, passando.
- Menu com cadastro de carro e patinete, simulação de aluguel e manutenção.
- Evidências em `docs/sprints/evidencias/`: saída do `dotnet test` e uma execução do menu mostrando os cálculos diferentes e as duas manutenções.
- Diagrama de classes atualizado em `docs/diagramas/`, com a hierarquia de veículos.
- Product Backlog atualizado.
- Seções G e H deste documento preenchidas ao fim da sprint, com o tempo real de cada tarefa.

## G. Resultado da sprint

A preencher ao fim da sprint. Para cada item ou tarefa:

- **Item ou tarefa:**
- **Situação:** (concluído, parcialmente concluído ou não concluído)
- **Evidência:**
- **Observações:**

## H. Retrospectiva

A preencher ao fim da sprint.

**O que funcionou bem?**

**Quais foram as principais dificuldades?**

**O planejamento foi adequado ao esforço necessário?**

**O que deverá ser melhorado na próxima sprint?**
