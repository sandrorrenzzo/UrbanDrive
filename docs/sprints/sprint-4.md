# Sprint 4: Abstração, interfaces e consolidação do sistema

## A. Identificação

- **Grupo:** individual
- **Integrantes:** Sandro Renzzo
- **Número da sprint:** 4
- **Período:** 04/11/2026 a 09/11/2026
- **Responsável pelo acompanhamento:** Sandro Renzzo

> A Sprint 3 terminou em 04/11/2026, antes do fim do período previsto. A Sprint 4 começa no mesmo dia e vai até 09/11/2026.

## B. Objetivo da sprint

Entregar a versão final e integrada do UrbanDrive: `Veiculo` passa a ser abstrata, as entidades geram relatórios por um contrato comum (`IImprimivel`), os veículos elétricos (patinete e carro elétrico) têm bateria e podem ser recarregados por outro contrato (`IEletrico`), e o menu reúne cadastro, consulta, relatórios, recarga e os cálculos polimórficos. Ao final, o projeto deve ter README de instalação e execução, relatório final e roteiro de apresentação, com a documentação correspondendo ao estado real do código.

## C. Itens selecionados do Product Backlog

**PB11**
- **ID:** PB11 (issue #11)
- **Requisito ou item:** RF02, `Veiculo` abstrata
- **Prioridade:** Média
- **Justificativa:** é a primeira tarefa da sprint porque afeta o código que já existe (menu e testes que criam `new Veiculo(...)`), pendência registrada na Sprint 3. Fazer antes evita retrabalho nas interfaces.

**PB12**
- **ID:** PB12 (issue #12)
- **Requisito ou item:** RF08, `IImprimivel` com `GerarRelatorio()`
- **Prioridade:** Média
- **Justificativa:** depende de `Motorista`, `Frota` e da hierarquia de veículos, todos concluídos. Alimenta a opção de relatórios do menu final.

**PB13**
- **ID:** PB13 (issue #13)
- **Requisito ou item:** RF09, `IEletrico` com `Recarregar()` em `Patinete` e `CarroEletrico`
- **Prioridade:** Média
- **Justificativa:** depende da hierarquia de veículos (PB08) e do cálculo de aluguel (PB09). É o último conceito de POO que falta.

**PB14**
- **ID:** PB14 (issue #14)
- **Requisito ou item:** RF10, Menu final integrado
- **Prioridade:** Alta
- **Justificativa:** é o incremento demonstrável da entrega final e depende de todos os itens anteriores.

**PB15**
- **ID:** PB15 (issue #15)
- **Requisito ou item:** README de execução e relatório final
- **Prioridade:** Baixa
- **Justificativa:** só pode ser feito com o produto pronto, mas é entregável obrigatório da sprint.

**Entidades que implementam `IImprimivel` e por quê**
- **`Veiculo`** (e, por herança, `Carro`, `Patinete` e `CarroEletrico`): é a entidade principal consultada no menu. Cada tipo acrescenta ao relatório as suas características (portas, autonomia, bateria).
- **`Motorista`:** o relatório mostra os dados pessoais e a CNH, que só existe dentro do motorista (composição).
- **`Frota`:** o relatório mostra a frota e os veículos que ela agrupa (agregação).
- **`Locacao`:** o relatório funciona como o recibo do aluguel: motorista, veículo, dias e valor calculado pelo tipo do veículo.
- `CNH` não implementa, porque não é consultada sozinha: aparece no relatório do motorista. `ConfiguradorSistema` também não, porque não é uma entidade.

**Regras dos veículos elétricos**
- `IEletrico` define o nível da bateria (em %), o consumo de bateria e o método `Recarregar()`, que volta a bateria para 100%.
- Todo veículo elétrico começa com 100% de bateria.
- Cada dia de locação consome 10% da bateria, sem passar de 0%.
- Um veículo elétrico com menos de 20% de bateria não pode ser locado até ser recarregado.
- **`CarroEletrico : Carro, IEletrico`:** reaproveita o cálculo do `Carro` (com seguro) e soma R$ 10,00 por dia de recarga. Exemplo com taxa de 10%: R$ 150,00 por 3 dias = 495,00 + 60,00 + 30,00 = R$ 585,00.
- `Patinete` passa a implementar `IEletrico`, mantendo o cálculo com desconto.

**Mudança no que já existe (refatoração registrada)**
- Com `Veiculo` abstrata, não existe mais "veículo comum". A opção do menu que cadastrava esse tipo sai, e os testes que criavam `new Veiculo(...)` passam a usar `Carro` ou uma classe de teste. Essa mudança será registrada na seção G.

## D. Planejamento das tarefas

Unidade utilizada nas estimativas: horas.

Seguindo a retrospectiva da Sprint 3, o tempo real de cada tarefa será anotado no momento em que a issue for movida para Done.

**T01**
- **ID:** T01
- **Tarefa:** tornar `Veiculo` abstrata, com uma propriedade abstrata `Tipo` definida em cada classe filha
- **Item relacionado:** PB11
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T02**
- **ID:** T02
- **Tarefa:** ajustar os testes, o serviço e o menu que instanciavam `Veiculo` diretamente
- **Item relacionado:** PB11
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T03**
- **ID:** T03
- **Tarefa:** criar `IImprimivel` em `Models/Interfaces` e implementar `GerarRelatorio()` em `Veiculo` (com acréscimos nas filhas), `Motorista`, `Frota` e `Locacao`
- **Item relacionado:** PB12
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T04**
- **ID:** T04
- **Tarefa:** escrever os testes dos relatórios, incluindo uma lista de `IImprimivel` com entidades diferentes
- **Item relacionado:** PB12
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T05**
- **ID:** T05
- **Tarefa:** criar `IEletrico` e fazer `Patinete` implementar, com bateria, consumo e recarga
- **Item relacionado:** PB13
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T06**
- **ID:** T06
- **Tarefa:** criar `CarroEletrico : Carro, IEletrico` com o cálculo de aluguel próprio
- **Item relacionado:** PB13
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T07**
- **ID:** T07
- **Tarefa:** aplicar o consumo e o bloqueio por bateria baixa na locação e escrever os testes de recarga, consumo e cálculo do carro elétrico
- **Item relacionado:** PB13
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T08**
- **ID:** T08
- **Tarefa:** adicionar ao `VeiculoService` o cadastro de carro elétrico e a recarga pelo Id, com testes
- **Item relacionado:** PB13, PB14
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T09**
- **ID:** T09
- **Tarefa:** finalizar o menu: cadastro por tipo, consulta, relatórios, recarga e demonstração dos cálculos polimórficos
- **Item relacionado:** PB14
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 1 h

**T10**
- **ID:** T10
- **Tarefa:** rodar a bateria completa de testes e o menu e salvar as evidências finais
- **Item relacionado:** PB14
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T11**
- **ID:** T11
- **Tarefa:** fazer o diagrama de classes final
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T12**
- **ID:** T12
- **Tarefa:** escrever no README os requisitos, a instalação e os comandos `dotnet run` e `dotnet test`
- **Item relacionado:** PB15
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T13**
- **ID:** T13
- **Tarefa:** escrever o relatório final com a evolução das sprints, as decisões e as dificuldades
- **Item relacionado:** PB15
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 1 h

**T14**
- **ID:** T14
- **Tarefa:** preparar o roteiro de apresentação, mostrando cada conceito de POO no código e no programa rodando
- **Item relacionado:** PB15
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T15**
- **ID:** T15
- **Tarefa:** revisar se a documentação corresponde ao código e testar o projeto clonando o repositório em uma pasta limpa e seguindo o README
- **Item relacionado:** PB15
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**T16**
- **ID:** T16
- **Tarefa:** atualizar o backlog e registrar o resultado e a retrospectiva da sprint
- **Item relacionado:** todos
- **Responsável:** Sandro Renzzo
- **Estimativa de esforço:** 0,5 h

**Total:** 9 h (3 h de abstração e interfaces, 2 h de serviço, menu e evidências, 3,5 h de documentação final e 0,5 h de encerramento)

## E. Critérios de aceitação

**PB11: `Veiculo` abstrata**
- `Veiculo` é `abstract`: `new Veiculo(...)` não compila.
- Cada classe concreta (`Carro`, `Patinete`, `CarroEletrico`) define a propriedade abstrata `Tipo`.
- Nenhum teste, serviço ou opção do menu instancia `Veiculo` diretamente, e todos os testes continuam passando.

**PB12: `IImprimivel`**
- A interface fica em `UrbanDrive.Models/Interfaces` e define `string GerarRelatorio()`.
- `Veiculo`, `Motorista`, `Frota` e `Locacao` implementam a interface; `Carro`, `Patinete` e `CarroEletrico` herdam e acrescentam as suas características.
- O relatório do motorista inclui a CNH; o da frota inclui os veículos; o da locação inclui o valor calculado pelo tipo do veículo.
- Uma `List<IImprimivel>` com entidades diferentes gera os relatórios sem verificar o tipo.
- Existe pelo menos um teste por entidade e todos passam.

**PB13: `IEletrico` e `CarroEletrico`**
- `Patinete` e `CarroEletrico` implementam `IEletrico`; `Carro` comum não implementa.
- Todo veículo elétrico começa com 100% de bateria; `Recarregar()` volta a bateria para 100%.
- Cada dia de locação consome 10% da bateria, sem ficar negativa.
- Locar um veículo elétrico com menos de 20% de bateria lança exceção.
- Com taxa de 10%, um carro elétrico de R$ 150,00 por 3 dias custa R$ 585,00, e o patinete mantém o cálculo com desconto.
- Existe pelo menos um teste para cada regra acima e todos passam.

**PB14: Menu final**
- O menu permite cadastrar carro, carro elétrico e patinete, e consultar veículos, motoristas, frota e locações.
- Há opções de relatórios, de recarga e de simulação do aluguel de todos os veículos.
- Todas as funcionalidades das sprints anteriores continuam disponíveis.
- O `Program.cs` continua sem regra de negócio.

**PB15: Documentação final**
- O README explica os requisitos, como clonar, rodar o programa (`dotnet run`) e rodar os testes (`dotnet test`).
- Clonando o repositório em uma pasta limpa e seguindo só o README, o programa e os testes rodam.
- O relatório final descreve a evolução das quatro sprints, as principais decisões e as dificuldades.
- O roteiro de apresentação indica onde cada conceito de POO aparece no código e no menu.
- A documentação corresponde ao estado real do código.

## F. Entregáveis previstos

- Código integrado com `Veiculo` abstrata, `IImprimivel`, `IEletrico` e `CarroEletrico`, na `master` por pull requests que fecham as issues #11 a #15 com `Closes #N`.
- Aplicação executável com o menu final.
- Bateria completa de testes passando e evidências finais em `docs/sprints/evidencias/`.
- Diagrama de classes final em `docs/diagramas/`.
- README com instalação e execução.
- Relatório final e roteiro de apresentação em `docs/`.
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
