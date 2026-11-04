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

O tempo real de cada tarefa não foi anotado nesta sprint, apesar de estar previsto na seção D. Por isso as tarefas abaixo não trazem esse campo (ver retrospectiva).

**T01**
- **Item ou tarefa:** T01, `Carro : Veiculo` com portas e `: base(...)` (PB08)
- **Situação:** concluído
- **Evidência:** PR #32, commit `7468a48`
- **Observações:** como o construtor da base roda primeiro, a validação das portas é feita dentro da chamada `base(...)`, para que um carro inválido não incremente o contador.

**T02**
- **Item ou tarefa:** T02, `sealed Patinete : Veiculo` com autonomia (PB08)
- **Situação:** concluído
- **Evidência:** PR #32, commit `9fce49e`
- **Observações:** mesma solução de T01 para a validação da autonomia.

**T03**
- **Item ou tarefa:** T03, testes de `Carro` e `Patinete` (PB08)
- **Situação:** concluído
- **Evidência:** PR #32, commit `64e56cb`
- **Observações:** 14 testes, incluindo `Patinete` selado e o contador.

**T04**
- **Item ou tarefa:** T04, `CalcularAluguel` virtual em `Veiculo` (PB09)
- **Situação:** concluído
- **Evidência:** PR #33, commit `c773fa0`
- **Observações:** nenhuma.

**T05**
- **Item ou tarefa:** T05, `override` em `Carro` e `Patinete` (PB09)
- **Situação:** concluído
- **Evidência:** PR #33, commit `f6630ea`
- **Observações:** os dois chamam `base.CalcularAluguel(dias)`, sem repetir a regra comum.

**T06**
- **Item ou tarefa:** T06, `Locacao` usando `CalcularAluguel` e testes de cálculo (PB09)
- **Situação:** concluído
- **Evidência:** PR #33, commit `088668a`
- **Observações:** resolve a pendência da Sprint 2. A verificação de estilo do build apontou o `using` que ficou sem uso na `Locacao`.

**T07**
- **Item ou tarefa:** T07, sobrecargas de `RealizarManutencao` com histórico (PB10)
- **Situação:** concluído
- **Evidência:** PR #34, commits `5f20db0` e `5202d82`
- **Observações:** a manutenção fica em `Veiculo` e vale para carro e patinete.

**T08**
- **Item ou tarefa:** T08, cadastro de carro e patinete no `VeiculoService`
- **Situação:** concluído
- **Evidência:** PR #35, commit `7c80db1`
- **Observações:** a manutenção pelo Id do veículo também foi colocada no serviço, para o menu não ter regra de negócio.

**T09**
- **Item ou tarefa:** T09, menu com tipos, simulação de aluguel e manutenção
- **Situação:** concluído
- **Evidência:** PR #35, commit `8015199`, `evidencias/sprint-3-execucao-menu.txt`
- **Observações:** a opção 12 mostra o polimorfismo: R$ 495,00, R$ 555,00 e R$ 79,20 para o mesmo período.

**T10**
- **Item ou tarefa:** T10, evidências
- **Situação:** concluído
- **Evidência:** `docs/sprints/evidencias/sprint-3-testes.txt` e `sprint-3-execucao-menu.txt`
- **Observações:** 96 testes aprovados, 0 falhas.

**T11**
- **Item ou tarefa:** T11, diagrama da hierarquia de veículos
- **Situação:** concluído
- **Evidência:** `docs/diagramas/classes-sprint-3.md`
- **Observações:** nenhuma.

**T12**
- **Item ou tarefa:** T12, backlog, resultado e retrospectiva
- **Situação:** concluído
- **Evidência:** `docs/backlog.md` e este documento
- **Observações:** nenhuma.

**Pendências para a Sprint 4**
- O menu ainda cadastra "veículo comum". Quando `Veiculo` virar abstrata (PB11), essa opção e os testes que criam `new Veiculo(...)` precisam ser ajustados.

## H. Retrospectiva

**O que funcionou bem?**
Colocar a regra comum em `Veiculo` e usar `base` nas classes filhas: `Carro` e `Patinete` ficaram curtas e só com o que é delas. Prever no planejamento o serviço e o menu evitou trabalho fora do plano, que foi o problema da Sprint 2. A verificação de estilo no build também ajudou a pegar código sobrando.

**Quais foram as principais dificuldades?**
Entender que o construtor da classe base roda antes do construtor da classe filha, o que fazia um carro inválido ser contado no `ConfiguradorSistema`. Também houve dois PRs (#32 e #34) que pareciam mergeados mas continuavam abertos, porque faltou confirmar o merge no GitHub.

**O planejamento foi adequado ao esforço necessário?**
Sim em escopo: todas as 12 tarefas foram concluídas, sem trabalho extra fora do planejado. A sprint terminou em dois dias (03/11 e 04/11), dentro do período. O tempo real por tarefa, porém, mais uma vez não foi anotado, então não dá para comparar com as 6,5 h estimadas.

**O que deverá ser melhorado na próxima sprint?**
- Anotar o tempo real no momento em que cada issue for movida para Done, já que só a intenção de anotar não funcionou em três sprints.
- Conferir que o PR mostra "Merged" antes de começar a tarefa seguinte.
- Tratar logo no início da Sprint 4 os pontos do código que criam `new Veiculo(...)`, por causa da classe abstrata.
