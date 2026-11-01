# Product Backlog UrbanDrive

Backlog montado a partir dos requisitos funcionais RF01 a RF10 do enunciado.
Cada item também existe como issue no GitHub e está no quadro [UrbanDrive](https://github.com/users/sandrorrenzzo/projects/1).

## Critérios

**Divisão dos itens:** cada item gera algo que pode ser testado ou demonstrado sozinho. Requisitos que envolvem conceitos de sprints diferentes foram separados. Exemplo: o RF05 virou PB07 (associação) e PB09 (polimorfismo).

**Prioridade:**
- Alta: desbloqueia outros itens ou torna a sprint demonstrável.
- Média: exigido, mas nenhum outro item depende dele.
- Baixa: só pode ser feito no fim.

**Sprint:** segue a ordem dos conteúdos de POO do enunciado (encapsulamento, relacionamentos, herança, interfaces) e as dependências entre os itens.

**Estimativa:** em horas, unidade usada em todas as sprints.

Diagramas, evidências de testes e documentos de cada sprint não são itens separados. Eles entram como tarefas no planejamento de cada sprint.

## Sprint 1 (total: 3 h)

**PB01: Estrutura da solution (Models, Services, App, Tests)**
- Issue: #1
- Requisito: nenhum (item técnico)
- Depende de: nenhum
- Prioridade: Alta
- Estimativa: 0,5 h
- Situação: Concluído

**PB02: `Veiculo` encapsulada com validações**
- Issue: #2
- Requisito: RF01
- Depende de: PB01
- Prioridade: Alta
- Estimativa: 1,5 h
- Situação: Concluído

**PB03: Menu de console para cadastrar e listar veículos**
- Issue: #3
- Requisito: RF10
- Depende de: PB02
- Prioridade: Alta
- Estimativa: 1 h
- Situação: Concluído

## Sprint 2 (total: 4,5 h)

**PB04: `Motorista` com `CNH` (composição)**
- Issue: #4
- Requisito: RF03
- Depende de: PB02
- Prioridade: Alta
- Estimativa: 1,5 h
- Situação: A fazer

**PB05: `Frota` com lista de veículos (agregação)**
- Issue: #5
- Requisito: RF04
- Depende de: PB02
- Prioridade: Alta
- Estimativa: 1 h
- Situação: A fazer

**PB06: `ConfiguradorSistema` com membros estáticos**
- Issue: #6
- Requisito: RF07
- Depende de: PB02
- Prioridade: Média
- Estimativa: 1 h
- Situação: A fazer

**PB07: `Locacao` ligando `Motorista` e `Veiculo` (associação)**
- Issue: #7
- Requisito: RF05
- Depende de: PB04
- Prioridade: Média
- Estimativa: 1 h
- Situação: A fazer

## Sprint 3 (total: 3,5 h)

**PB08: `Carro` e `Patinete` (`sealed`) herdando de `Veiculo`**
- Issue: #8
- Requisito: RF02
- Depende de: PB02
- Prioridade: Alta
- Estimativa: 1,5 h
- Situação: A fazer

**PB09: `CalcularAluguel(int dias)` com cálculo por tipo**
- Issue: #9
- Requisito: RF05
- Depende de: PB06, PB08
- Prioridade: Alta
- Estimativa: 1,5 h
- Situação: A fazer

**PB10: `RealizarManutencao()` e `RealizarManutencao(string)`**
- Issue: #10
- Requisito: RF06
- Depende de: PB02
- Prioridade: Média
- Estimativa: 0,5 h
- Situação: A fazer

## Sprint 4 (total: 6,5 h)

**PB11: `Veiculo` abstrata**
- Issue: #11
- Requisito: RF02
- Depende de: PB08
- Prioridade: Média
- Estimativa: 0,5 h
- Situação: A fazer

**PB12: `IImprimivel` com `GerarRelatorio()`**
- Issue: #12
- Requisito: RF08
- Depende de: PB04, PB05, PB08
- Prioridade: Média
- Estimativa: 1 h
- Situação: A fazer

**PB13: `IEletrico` com `Recarregar()` em `Patinete` e `CarroEletrico`**
- Issue: #13
- Requisito: RF09
- Depende de: PB08
- Prioridade: Média
- Estimativa: 1,5 h
- Situação: A fazer

**PB14: Menu final integrado**
- Issue: #14
- Requisito: RF10
- Depende de: PB03 a PB13
- Prioridade: Alta
- Estimativa: 1,5 h
- Situação: A fazer

**PB15: README de execução e relatório final**
- Issue: #15
- Requisito: nenhum (item de documentação)
- Depende de: PB14
- Prioridade: Baixa
- Estimativa: 2 h
- Situação: A fazer

## Histórico de alterações

- 31/10/2026: criação do backlog inicial.
- 31/10/2026: revisão das estimativas, de 34 h para 17,5 h no total, por considerar as primeiras muito altas para o tamanho de cada item.
- 31/10/2026: fim da Sprint 1. PB01, PB02 e PB03 concluídos.
