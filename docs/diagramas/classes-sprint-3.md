# Diagrama de classes: Sprint 3

Estado do código ao fim da Sprint 3. Membros com `-` são privados, com `+` são públicos e com `~` são `internal`. Membros sublinhados (`$`) são estáticos. Métodos em itálico (`*`) são `virtual`.

## Hierarquia de veículos

```mermaid
classDiagram
    direction TB

    class Veiculo {
        -int _id
        -string _placa
        -string _modelo
        -decimal _valorDiaria
        -List~string~ _historicoManutencoes
        +int Id
        +string Placa
        +string Modelo
        +decimal ValorDiaria
        +IReadOnlyList~string~ HistoricoManutencoes
        +Veiculo(int id, string placa, string modelo, decimal valorDiaria)
        +CalcularAluguel(int dias)* decimal
        +RealizarManutencao() string
        +RealizarManutencao(string pecaSubstituida) string
        -RegistrarManutencao(string descricao) string
    }

    class Carro {
        -decimal SeguroPorDia = 20
        -int _quantidadePortas
        +int QuantidadePortas
        +Carro(int id, string placa, string modelo, decimal valorDiaria, int quantidadePortas)
        +CalcularAluguel(int dias) decimal
    }

    class Patinete {
        <<sealed>>
        -decimal Desconto = 0,20
        -int _autonomiaKm
        +int AutonomiaKm
        +Patinete(int id, string placa, string modelo, decimal valorDiaria, int autonomiaKm)
        +CalcularAluguel(int dias) decimal
    }

    class ConfiguradorSistema {
        <<static>>
        +decimal TaxaServico$
        +int TotalVeiculos$
        ~RegistrarVeiculoCriado()$
    }

    Veiculo <|-- Carro : herança
    Veiculo <|-- Patinete : herança
    Veiculo ..> ConfiguradorSistema : usa a taxa e incrementa o contador
```

## Relacionamentos com as outras classes

```mermaid
classDiagram
    direction LR

    class Veiculo
    class Carro
    class Patinete
    class Motorista
    class CNH
    class Frota
    class Locacao {
        +decimal ValorTotal
    }

    Veiculo <|-- Carro
    Veiculo <|-- Patinete
    Motorista *-- "1" CNH : composição
    Frota o-- "0..*" Veiculo : agregação
    Locacao --> "1" Motorista : associação
    Locacao --> "1" Veiculo : associação
```

`Locacao` e `Frota` continuam guardando `Veiculo`, sem saber se o objeto é um carro, um patinete ou um veículo comum.

## Como cada conceito aparece

- **Herança:** `Carro` e `Patinete` herdam Id, placa, modelo, diária, validações, contador e manutenção de `Veiculo`. Os construtores usam `: base(...)`.
- **Sobrescrita (`override`):** `CalcularAluguel` é `virtual` em `Veiculo`. `Carro` soma o seguro e `Patinete` aplica o desconto, os dois chamando `base.CalcularAluguel(dias)`.
- **Polimorfismo:** `Locacao.ValorTotal` e a opção 12 do menu chamam `CalcularAluguel` em uma referência do tipo `Veiculo`, e o C# executa a versão do tipo real do objeto.
- **Sobrecarga:** `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)` têm o mesmo nome e parâmetros diferentes.
- **Classe selada:** `Patinete` é `sealed`, então nenhuma classe pode herdar dele.

## Regras de aluguel (taxa de 10%)

- **Veículo comum:** 150 × 3 × 1,10 = R$ 495,00
- **Carro:** 495,00 + 20 × 3 = R$ 555,00
- **Patinete:** 30 × 3 × 1,10 = 99,00 × 0,80 = R$ 79,20
