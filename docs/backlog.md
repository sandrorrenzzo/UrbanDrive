# Product Backlog — UrbanDrive

Backlog montado a partir dos requisitos funcionais RF01 a RF10 do enunciado.
Cada item também existe como issue no GitHub e está no quadro [UrbanDrive](https://github.com/users/sandrorrenzzo/projects/1).

## Critérios

- **Divisão dos itens:** cada item gera algo que pode ser testado ou demonstrado sozinho. Requisitos que envolvem conceitos de sprints diferentes foram separados (ex.: RF05 virou PB07, associação, e PB09, polimorfismo).
- **Prioridade:**
  - **Alta:** desbloqueia outros itens ou torna a sprint demonstrável.
  - **Média:** exigido, mas nenhum outro item depende dele.
  - **Baixa:** só pode ser feito no fim.
- **Sprint:** segue a ordem dos conteúdos de POO do enunciado (encapsulamento → relacionamentos → herança → interfaces) e as dependências entre os itens.
- **Estimativa:** em horas, unidade usada em todas as sprints.

Diagramas, evidências de testes e documentos de cada sprint não são itens separados: entram como tarefas no planejamento de cada sprint.

## Itens

| ID | Issue | Item | RF | Depende de | Prioridade | Sprint | Estimativa (h) | Situação |
|---|---|---|---|---|---|---|---|---|
| PB01 | #1 | Estrutura da solution (Models, Services, App, Tests) | — | — | Alta | 1 | 1 | A fazer |
| PB02 | #2 | `Veiculo` encapsulada com validações | RF01 | PB01 | Alta | 1 | 3 | A fazer |
| PB03 | #3 | Menu de console para cadastrar e listar veículos | RF10 | PB02 | Alta | 1 | 2 | A fazer |
| PB04 | #4 | `Motorista` com `CNH` (composição) | RF03 | PB02 | Alta | 2 | 3 | A fazer |
| PB05 | #5 | `Frota` com lista de veículos (agregação) | RF04 | PB02 | Alta | 2 | 2 | A fazer |
| PB06 | #6 | `ConfiguradorSistema` com membros estáticos | RF07 | PB02 | Média | 2 | 2 | A fazer |
| PB07 | #7 | `Locacao` ligando `Motorista` e `Veiculo` (associação) | RF05 | PB04 | Média | 2 | 2 | A fazer |
| PB08 | #8 | `Carro` e `Patinete` (`sealed`) herdando de `Veiculo` | RF02 | PB02 | Alta | 3 | 3 | A fazer |
| PB09 | #9 | `CalcularAluguel(int dias)` com cálculo por tipo | RF05 | PB06, PB08 | Alta | 3 | 3 | A fazer |
| PB10 | #10 | `RealizarManutencao()` e `RealizarManutencao(string)` | RF06 | PB02 | Média | 3 | 1 | A fazer |
| PB11 | #11 | `Veiculo` abstrata | RF02 | PB08 | Média | 4 | 1 | A fazer |
| PB12 | #12 | `IImprimivel` com `GerarRelatorio()` | RF08 | PB04, PB05, PB08 | Média | 4 | 2 | A fazer |
| PB13 | #13 | `IEletrico` com `Recarregar()` em `Patinete` e `CarroEletrico` | RF09 | PB08 | Média | 4 | 3 | A fazer |
| PB14 | #14 | Menu final integrado | RF10 | PB03 a PB13 | Alta | 4 | 3 | A fazer |
| PB15 | #15 | README de execução e relatório final | — | PB14 | Baixa | 4 | 3 | A fazer |

**Total por sprint:** Sprint 1: 6 h · Sprint 2: 9 h · Sprint 3: 7 h · Sprint 4: 12 h

## Histórico de alterações

| Data | Alteração |
|---|---|
| 31/10/2026 | Criação do backlog inicial. |
