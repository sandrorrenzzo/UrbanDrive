# Sprint 1: Modelagem inicial e encapsulamento

## A. Identificação

- **Grupo:** individual
- **Integrantes:** Sandro Renzzo
- **Número da sprint:** 1
- **Período:** 23/10/2026 a 31/10/2026
- **Responsável pelo acompanhamento:** Sandro Renzzo

## B. Objetivo da sprint

Ter uma primeira versão do UrbanDrive rodando no console, onde é possível cadastrar e listar veículos, com a classe `Veiculo` protegendo seus dados: um veículo com placa nula ou vazia, modelo vazio, identificador inválido ou diária não positiva não pode ser criado.

## C. Itens selecionados do Product Backlog

**PB01**
- **ID:** PB01 (issue #1)
- **Requisito ou item:** Estrutura da solution (Models, Services, App, Tests)
- **Prioridade:** Alta
- **Justificativa:** é pré-requisito de todos os outros itens. Sem os projetos separados não dá para manter a lógica fora do `Program.cs` nem escrever testes.

**PB02**
- **ID:** PB02 (issue #2)
- **Requisito ou item:** RF01, `Veiculo` encapsulada com validações
- **Prioridade:** Alta
- **Justificativa:** é a entidade central do sistema. `Frota`, `Locacao`, `Carro`, `Patinete` e as interfaces dependem dela. Também é o conteúdo da aula desta sprint (encapsulamento).

**PB03**
- **ID:** PB03 (issue #3)
- **Requisito ou item:** RF10, Menu de console para cadastrar e listar veículos
- **Prioridade:** Alta
- **Justificativa:** torna o incremento demonstrável. Depende só de PB02 e o esforço é pequeno.

Os demais itens ficam para as próximas sprints porque dependem de `Veiculo` pronta e de conteúdos ainda não vistos (relacionamentos, herança, interfaces).

## D. Planejamento das tarefas

Unidade utilizada nas estimativas: horas.

**T01**
- **ID:** T01
- **Tarefa:** criar os projetos Models, Services, App e Tests e ligar as referências entre eles
- **Item relacionado:** PB01
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T02**
- **ID:** T02
- **Tarefa:** criar a classe `Veiculo` com campos privados, propriedades públicas e construtor
- **Item relacionado:** PB02
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T03**
- **ID:** T03
- **Tarefa:** implementar as validações de Id, Placa, Modelo e ValorDiaria
- **Item relacionado:** PB02
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T04**
- **ID:** T04
- **Tarefa:** escrever os testes unitários de criação e de cada validação
- **Item relacionado:** PB02
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T05**
- **ID:** T05
- **Tarefa:** criar o serviço de cadastro e listagem de veículos em `UrbanDrive.Services`
- **Item relacionado:** PB03
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T06**
- **ID:** T06
- **Tarefa:** criar o menu de console (cadastrar, listar, sair) tratando os erros de validação
- **Item relacionado:** PB03
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T07**
- **ID:** T07
- **Tarefa:** rodar o programa e os testes e salvar as evidências
- **Item relacionado:** PB02, PB03
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T08**
- **ID:** T08
- **Tarefa:** fazer o diagrama de classes inicial
- **Item relacionado:** PB02
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T09**
- **ID:** T09
- **Tarefa:** registrar o resultado e a retrospectiva da sprint
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**Total:** 4,5 h (3 h dos itens do backlog e 1,5 h de encerramento)

## E. Critérios de aceitação

**PB01: Estrutura da solution**
- `dotnet build` na raiz termina sem erros.
- `dotnet test` na raiz encontra e executa o projeto de testes.

**PB02: `Veiculo` encapsulada com validações**
- Os dados ficam em campos `private`; de fora da classe só se acessa pelas propriedades públicas.
- O construtor recebe Id, Placa, Modelo e ValorDiaria e cria o objeto com esses valores.
- Placa nula, vazia ou só com espaços lança exceção e o objeto não é criado.
- Modelo nulo ou vazio lança exceção.
- Id menor ou igual a zero lança exceção.
- ValorDiaria menor ou igual a zero lança exceção.
- Alterar uma propriedade para um valor inválido depois de criado lança exceção e mantém o valor anterior.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB03: Menu de console**
- É possível cadastrar um veículo informando placa, modelo e valor da diária.
- É possível listar todos os veículos cadastrados.
- Com dados inválidos, o programa mostra a mensagem de erro e continua rodando.
- A lógica de cadastro fica fora do `Program.cs`.

## F. Entregáveis previstos

- Código com `Veiculo`, o serviço de cadastro e o menu de console, integrado na `master` por pull requests que fecham as issues #1, #2 e #3.
- Testes unitários de `Veiculo` passando.
- Evidências em `docs/sprints/evidencias/`: saída do `dotnet test` e uma execução do menu com um cadastro válido, um cadastro recusado e a listagem.
- Diagrama de classes inicial em `docs/diagramas/`.
- Seções G e H deste documento preenchidas ao fim da sprint.

## G. Resultado da sprint

**T01**
- **Item ou tarefa:** T01, criar os projetos e ligar as referências (PB01)
- **Situação:** concluído
- **Evidência:** PR #18, commit `6b77c8d`
- **Observações:** o projeto console criado pelo Visual Studio foi substituído pelos quatro projetos.

**T02**
- **Item ou tarefa:** T02, classe `Veiculo` com campos privados, propriedades públicas e construtor (PB02)
- **Situação:** concluído
- **Evidência:** PR #19, commit `e998b52`
- **Observações:** nenhuma.

**T03**
- **Item ou tarefa:** T03, validações de Id, Placa, Modelo e ValorDiaria (PB02)
- **Situação:** concluído
- **Evidência:** PR #19, commit `5103645`
- **Observações:** além da placa nula ou vazia exigida no enunciado, também foram validados Id, Modelo e ValorDiaria.

**T04**
- **Item ou tarefa:** T04, testes unitários de `Veiculo` (PB02)
- **Situação:** concluído
- **Evidência:** PR #19, commit `cea690b`, `evidencias/sprint-1-testes.txt`
- **Observações:** 12 casos de teste cobrindo todas as regras da seção E.

**T05**
- **Item ou tarefa:** T05, serviço de cadastro e listagem (PB03)
- **Situação:** concluído
- **Evidência:** PR #20, commit `068d9ec`
- **Observações:** foram incluídos 2 testes para o serviço, que não estavam previstos no planejamento.

**T06**
- **Item ou tarefa:** T06, menu de console (PB03)
- **Situação:** concluído
- **Evidência:** PR #20, commit `7e45c4d`, `evidencias/sprint-1-execucao-menu.txt`
- **Observações:** o menu mostra a mensagem de erro e continua rodando quando os dados são inválidos.

**T07**
- **Item ou tarefa:** T07, evidências
- **Situação:** concluído
- **Evidência:** `docs/sprints/evidencias/`
- **Observações:** 14 testes aprovados, 0 falhas. Evidências salvas em texto.

**T08**
- **Item ou tarefa:** T08, diagrama de classes inicial
- **Situação:** concluído
- **Evidência:** `docs/diagramas/classes-sprint-1.md`
- **Observações:** diagrama feito em Mermaid, exibido direto no GitHub.

**T09**
- **Item ou tarefa:** T09, resultado e retrospectiva
- **Situação:** concluído
- **Evidência:** este documento
- **Observações:** nenhuma.

**Pendências para a Sprint 2**
- As issues #1, #2 e #3 não foram fechadas automaticamente pelos PRs, porque as descrições não tinham `Closes #N`. Foram fechadas manualmente.
- O Id dos veículos é gerado dentro do `VeiculoService`. Avaliar se essa responsabilidade passa para o `ConfiguradorSistema` (PB06).

## H. Retrospectiva

**O que funcionou bem?**
Separar a solution em Models, Services, App e Tests logo no começo. O `Program.cs` ficou só com o menu e as validações do `Veiculo` puderam ser testadas sem rodar o programa. Também ajudou fazer um PR por item do backlog, com commits pequenos para cada tarefa.

**Quais foram as principais dificuldades?**
O trabalho ficou concentrado no fim do prazo: o planejamento só foi registrado no último dia da sprint (31/10), junto com o código. Também foi preciso refazer a solution, porque o Visual Studio criou um único projeto console. Por fim, os PRs foram abertos sem `Closes #N` e as issues tiveram que ser fechadas manualmente.

**O planejamento foi adequado ao esforço necessário?**
Em escopo, sim: todos os itens selecionados (PB01, PB02 e PB03) foram concluídos. As estimativas foram revistas antes de começar, de 34 h para 17,5 h no backlog, por estarem altas para o tamanho dos itens. O tempo real de cada tarefa não foi anotado, então não dá para comparar com as 4,5 h planejadas.

**O que deverá ser melhorado na próxima sprint?**
- Registrar o planejamento no início da sprint, antes de começar o código.
- Colocar `Closes #N` na descrição de cada PR para fechar a issue automaticamente.
- Anotar o tempo real gasto em cada tarefa para comparar com a estimativa.
