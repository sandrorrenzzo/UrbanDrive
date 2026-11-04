# Relatório final: UrbanDrive

## Visão geral

O UrbanDrive é um sistema de console em C# para gestão de mobilidade urbana: cadastro de carros, carros elétricos e patinetes, motoristas com CNH, frota, locações com cálculo por tipo de veículo, manutenção, recarga de veículos elétricos e relatórios.

O trabalho foi feito individualmente, em quatro sprints, entre 23/10/2026 e 04/11/2026. Cada sprint teve planejamento (seções A a F), resultado (G) e retrospectiva (H), versionados em `docs/sprints/`. O código foi integrado na `master` por pull requests, cada um ligado a uma issue do Product Backlog e acompanhado no quadro do GitHub Projects.

**Números da entrega**
- 15 itens no Product Backlog (PB01 a PB15), todos concluídos.
- 4 sprints com planejamento, resultado e retrospectiva.
- 118 testes automatizados (xUnit), todos aprovados.
- Pull requests #16 a #42, um por item, ajuste ou documento.

## Evolução das sprints

**Sprint 1: modelagem inicial e encapsulamento (23/10 a 31/10)**
- Estrutura da solution em quatro projetos: Models, Services, App e Tests.
- Classe `Veiculo` com campos privados, propriedades públicas, construtor e validações (placa nula ou vazia, modelo, Id e diária).
- Primeiro menu de console para cadastrar e listar veículos.
- Resultado: 14 testes.

**Sprint 2: relacionamentos e membros estáticos (02/11 a 03/11)**
- Composição: `Motorista` cria a `CNH` dentro do construtor, e a `CNH` não pode ser criada fora do projeto Models.
- Agregação: `Frota` guarda veículos que existem por conta própria.
- Associação: `Locacao` liga um motorista e um veículo.
- Membros estáticos: `ConfiguradorSistema` com a taxa de serviço e o contador de veículos.
- Serviços de motorista, frota e locação e menu ampliado.
- Resultado: 63 testes.

**Sprint 3: herança, polimorfismo e especialização (03/11 a 04/11)**
- `Carro` e `Patinete` (`sealed`) herdando de `Veiculo` com `: base(...)`.
- `CalcularAluguel(int dias)` `virtual` em `Veiculo` e sobrescrito: seguro de R$ 20,00 por dia no carro e 20% de desconto no patinete.
- Sobrecarga de `RealizarManutencao`, com e sem peça substituída.
- A `Locacao` passou a usar o cálculo do tipo real do veículo.
- Resultado: 96 testes.

**Sprint 4: abstração, interfaces e consolidação (04/11)**
- `Veiculo` passou a ser `abstract`, com a propriedade abstrata `Tipo`.
- `IImprimivel` com `GerarRelatorio()` em `Veiculo`, `Motorista`, `Frota` e `Locacao`.
- `IEletrico` em `Patinete` e no novo `CarroEletrico : Carro, IEletrico`, com bateria que é consumida nas locações e recarregada.
- Menu final organizado em seções, com relatórios e recarga.
- README, diagrama final, este relatório e o roteiro de apresentação.
- Resultado: 118 testes.

## Principais decisões

- **Separação em camadas desde o início.** As regras ficam em `UrbanDrive.Models` e `UrbanDrive.Services`; o `Program.cs` só lê os dados, chama os serviços e mostra o resultado. Isso permitiu testar quase tudo sem rodar o programa.
- **Validação nas propriedades.** Toda alteração passa pelo `set`, inclusive no construtor, então um objeto nunca fica em estado inválido.
- **Id gerado pelo contador estático.** O contador de veículos começou dentro do `VeiculoService` e passou para o `ConfiguradorSistema` na Sprint 2, para ser uma informação compartilhada por todo o sistema.
- **Validação antes do construtor da base.** Como o construtor de `Veiculo` roda antes do de `Carro` e `Patinete`, as validações próprias (portas e autonomia) são feitas dentro da chamada `base(...)`, para que um objeto inválido não seja contado.
- **Regra comum na classe-base.** `Carro` e `Patinete` chamam `base.CalcularAluguel(dias)` e só acrescentam a sua diferença, sem duplicar o cálculo.
- **Bateria por composição.** `Patinete` e `CarroEletrico` têm bases diferentes, então não podiam herdar a lógica de bateria de um lugar comum. Cada um tem uma `Bateria`, e as regras ficam numa classe só.
- **`ValorTotal` calculado na leitura.** A locação consulta a taxa no momento do cálculo. Isso demonstra o membro estático compartilhado: mudar a taxa muda o valor.
- **Classe de teste para `Veiculo`.** Com `Veiculo` abstrata, os testes da regra comum passaram a usar `VeiculoDeTeste`, que existe só no projeto de testes.
- **Padrão de código verificado no build.** Um `.editorconfig` com as regras de nomes e estilo é aplicado pelo `dotnet build` e pelo `dotnet format`.

## Dificuldades encontradas

- **Planejamento tardio na Sprint 1.** O planejamento e o código da Sprint 1 foram registrados no mesmo dia, o último da sprint. Nas sprints seguintes o planejamento passou a ser commitado antes do código.
- **Diferença entre composição, agregação e associação na prática,** e o próprio conceito de locação no modelo.
- **Estado estático nos testes.** O contador e a taxa compartilhados fizeram os testes interferirem uns nos outros quando rodavam em paralelo. A execução paralela foi desativada e os testes que mudam a taxa devolvem o valor original no fim.
- **Ordem dos construtores na herança,** que fazia um carro inválido ser contado no `ConfiguradorSistema`.
- **Configuração do ambiente.** A formatação do VS Code usava a indentação detectada no arquivo em vez da regra do projeto.
- **Processo no GitHub.** Os primeiros PRs foram abertos sem `Closes #N`, e dois PRs (#32 e #34) pareciam mergeados mas continuavam abertos por falta de confirmação.
- **Tempo real das tarefas não foi anotado em nenhuma sprint,** mesmo com a melhoria registrada nas retrospectivas. Por isso não é possível comparar o esforço real com as estimativas em horas.

## Diferenças entre o planejado e o realizado

- **Sprint 2:** além dos itens planejados, foram criados os serviços de motorista, frota e locação, o padrão de código (`.editorconfig`) e a reorganização dos testes em pastas.
- **Sprint 3 e Sprint 4:** o trabalho seguiu o planejado, porque os serviços e o menu já estavam previstos nas tarefas.
- **Datas:** as Sprints 2, 3 e 4 terminaram antes do fim dos períodos previstos, e cada sprint seguinte começou no dia em que a anterior terminou. Essas mudanças estão registradas na seção A de cada planejamento.

## Ferramentas e fontes utilizadas

- C# com .NET 10, xUnit para os testes, Git e GitHub (issues, pull requests e GitHub Projects) para versionamento e acompanhamento, Mermaid para os diagramas.
- Documentação oficial da Microsoft sobre C# e .NET.

## Conclusão

O UrbanDrive chegou à versão integrada com todos os conceitos de POO pedidos: encapsulamento, composição, agregação, associação, membros estáticos, herança, sobrescrita, sobrecarga, polimorfismo, classe selada, classe abstrata e interfaces. O principal ponto a melhorar em trabalhos futuros é o acompanhamento do esforço: registrar o tempo real de cada tarefa no momento em que ela é concluída.
