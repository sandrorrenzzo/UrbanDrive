# Sprint 2: Relacionamentos entre objetos e membros estáticos

## A. Identificação

- **Grupo:** individual
- **Integrantes:** Sandro Renzzo
- **Número da sprint:** 2
- **Período:** 01/11/2026 a 09/11/2026
- **Responsável pelo acompanhamento:** Sandro Renzzo

## B. Objetivo da sprint

Ampliar o UrbanDrive para que os objetos se relacionem: um motorista com sua CNH, uma frota com seus veículos e uma locação ligando um motorista a um veículo, com o valor calculado a partir de uma taxa compartilhada por todo o sistema e um contador que registra quantos veículos foram criados. Ao final, essas interações devem ser demonstradas no console e verificadas por testes.

## C. Itens selecionados do Product Backlog

**PB04**
- **ID:** PB04 (issue #4)
- **Requisito ou item:** RF03, `Motorista` com `CNH` (composição)
- **Prioridade:** Alta
- **Justificativa:** é pré-requisito da `Locacao` (PB07). Não depende de nada além do que já existe e é o primeiro exemplo de composição pedido na sprint.

**PB05**
- **ID:** PB05 (issue #5)
- **Requisito ou item:** RF04, `Frota` com lista de veículos (agregação)
- **Prioridade:** Alta
- **Justificativa:** usa a classe `Veiculo` da Sprint 1 sem alterá-la. É o exemplo de agregação pedido na sprint e será usada nos relatórios da Sprint 4.

**PB06**
- **ID:** PB06 (issue #6)
- **Requisito ou item:** RF07, `ConfiguradorSistema` com membros estáticos
- **Prioridade:** Média
- **Justificativa:** cobre os membros estáticos pedidos na sprint. Também resolve uma pendência da Sprint 1: o controle de quantos veículos foram criados sai do `VeiculoService` e passa a ser uma informação compartilhada do sistema. A taxa será usada na `Locacao` (PB07) e no cálculo de aluguel da Sprint 3 (PB09).

**PB07**
- **ID:** PB07 (issue #7)
- **Requisito ou item:** RF05, `Locacao` ligando `Motorista` e `Veiculo` (associação)
- **Prioridade:** Média
- **Justificativa:** depende de PB04 e PB06, por isso é o último item da sprint. É o exemplo de associação e o ponto onde todos os objetos novos se encontram.

**Como os novos componentes se relacionam com a estrutura existente**
- `CNH` só pode ser criada pelo `Motorista`. Quando o motorista deixa de existir, a CNH deixa junto (composição).
- `Frota` guarda referências a objetos `Veiculo` que já existem. Remover um veículo da frota não apaga o veículo, e o mesmo veículo pode ser criado antes de entrar na frota (agregação).
- `Locacao` recebe um `Motorista` e um `Veiculo` já existentes e só os utiliza; nenhum dos dois pertence à locação (associação).
- `Veiculo` passa a avisar o `ConfiguradorSistema` sempre que um objeto é criado com sucesso, incrementando o contador estático.
- Todas as novas classes ficam em `UrbanDrive.Models`, mantendo o padrão da Sprint 1.

## D. Planejamento das tarefas

Unidade utilizada nas estimativas: horas.

**T01**
- **ID:** T01
- **Tarefa:** criar a classe `CNH` (número, categoria e validade) com validações, sem permitir que seja criada fora do projeto Models
- **Item relacionado:** PB04
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T02**
- **ID:** T02
- **Tarefa:** criar a classe `Motorista` (nome e CPF), que cria a sua `CNH` dentro do construtor
- **Item relacionado:** PB04
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T03**
- **ID:** T03
- **Tarefa:** escrever os testes de `Motorista` e `CNH`
- **Item relacionado:** PB04
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T04**
- **ID:** T04
- **Tarefa:** criar a classe `Frota` com lista privada de veículos e os métodos adicionar, remover e listar
- **Item relacionado:** PB05
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T05**
- **ID:** T05
- **Tarefa:** escrever os testes de `Frota`
- **Item relacionado:** PB05
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T06**
- **ID:** T06
- **Tarefa:** criar o `ConfiguradorSistema` com a taxa e o contador estáticos e fazer o construtor de `Veiculo` incrementar o contador
- **Item relacionado:** PB06
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T07**
- **ID:** T07
- **Tarefa:** escrever os testes do contador e da taxa compartilhada
- **Item relacionado:** PB06
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T08**
- **ID:** T08
- **Tarefa:** criar a classe `Locacao` com motorista, veículo, quantidade de dias e cálculo do valor total usando a taxa do `ConfiguradorSistema`
- **Item relacionado:** PB07
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T09**
- **ID:** T09
- **Tarefa:** escrever os testes de `Locacao`
- **Item relacionado:** PB07
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T10**
- **ID:** T10
- **Tarefa:** adicionar ao menu as opções de cadastrar motorista, gerenciar a frota, realizar locação e mostrar o contador e a taxa
- **Item relacionado:** PB04, PB05, PB06, PB07
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 1 h

**T11**
- **ID:** T11
- **Tarefa:** rodar o programa e os testes e salvar as evidências
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T12**
- **ID:** T12
- **Tarefa:** atualizar o diagrama de classes com associação, agregação e composição
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T13**
- **ID:** T13
- **Tarefa:** atualizar o backlog e registrar o resultado e a retrospectiva da sprint
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**Total:** 7 h (4,5 h dos itens do backlog, 1 h de menu e 1,5 h de encerramento)

## E. Critérios de aceitação

**PB04: `Motorista` com `CNH`**
- Ao criar um `Motorista`, a `CNH` dele é criada no mesmo momento, dentro do construtor.
- Não existe forma de criar uma `CNH` fora do projeto Models nem de trocar a CNH de um motorista depois de criado.
- Nome e CPF vazios lançam exceção. Número da CNH vazio, categoria inválida ou validade vencida lançam exceção e o motorista não é criado.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB05: `Frota` com lista de veículos**
- É possível adicionar, remover e listar veículos da frota.
- A lista de veículos não pode ser alterada de fora da classe, só pelos métodos da `Frota`.
- Adicionar o mesmo veículo duas vezes ou adicionar `null` lança exceção.
- Remover um veículo da frota não afeta o objeto `Veiculo`, que continua existindo.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB06: `ConfiguradorSistema`**
- A taxa e o contador são membros estáticos: o mesmo valor é visto em qualquer parte do sistema.
- O contador aumenta em 1 a cada `Veiculo` criado com sucesso e não aumenta quando a criação falha por validação.
- O contador não pode ser alterado diretamente de fora do projeto Models.
- Taxa negativa lança exceção.
- Existe pelo menos um teste para o contador e um para a taxa compartilhada, e todos passam.

**PB07: `Locacao`**
- Uma locação só é criada com um motorista, um veículo e uma quantidade de dias maior que zero.
- O valor total é igual a diária do veículo vezes a quantidade de dias, acrescido da taxa do `ConfiguradorSistema`.
- Mudar a taxa no `ConfiguradorSistema` muda o valor das locações calculadas depois, sem alterar o código da `Locacao`.
- Existe pelo menos um teste de cálculo e um de validação, e todos passam.

**Integração entre os objetos**
- Um teste cria um motorista, adiciona veículos a uma frota, realiza uma locação com um desses veículos e confere o valor e o contador.
- O menu permite fazer o mesmo fluxo pelo console.

## F. Entregáveis previstos

- Código com `CNH`, `Motorista`, `Frota`, `ConfiguradorSistema` e `Locacao`, integrado na `master` por pull requests que fecham as issues #4, #5, #6 e #7 com `Closes #N`.
- Testes unitários e de integração passando.
- Menu de console com as novas opções.
- Evidências em `docs/sprints/evidencias/`: saída do `dotnet test` e uma execução do menu mostrando cadastro de motorista, frota, locação e contador.
- Diagrama de classes atualizado em `docs/diagramas/`, com associação, agregação e composição.
- Product Backlog atualizado.
- Seções G e H deste documento preenchidas ao fim da sprint.

## G. Resultado da sprint

A preencher ao fim da sprint.

## H. Retrospectiva

A preencher ao fim da sprint.

**O que funcionou bem?**

**Quais foram as principais dificuldades?**

**O planejamento foi adequado ao esforço necessário?**

**O que deverá ser melhorado na próxima sprint?**
