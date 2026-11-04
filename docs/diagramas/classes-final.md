# Diagrama de classes final

Estado do código na entrega final (Sprint 4). Membros com `-` são privados, com `+` são públicos, com `#` são protegidos e com `~` são `internal`. Membros sublinhados (`$`) são estáticos. Métodos em itálico (`*`) são `virtual` ou `abstract`.

## Modelos

```mermaid
classDiagram
    direction TB

    class IImprimivel {
        <<interface>>
        +GerarRelatorio() string
    }

    class IEletrico {
        <<interface>>
        +int NivelBateria
        +ConsumirBateria(int dias)
        +Recarregar()
    }

    class Veiculo {
        <<abstract>>
        -int _id
        -string _placa
        -string _modelo
        -decimal _valorDiaria
        -List~string~ _historicoManutencoes
        +int Id
        +string Placa
        +string Modelo
        +decimal ValorDiaria
        +string Tipo*
        +IReadOnlyList~string~ HistoricoManutencoes
        #Veiculo(int id, string placa, string modelo, decimal valorDiaria)
        +CalcularAluguel(int dias)* decimal
        +RealizarManutencao() string
        +RealizarManutencao(string pecaSubstituida) string
        +GerarRelatorio()* string
    }

    class Carro {
        -int _quantidadePortas
        +int QuantidadePortas
        +Carro(int id, string placa, string modelo, decimal valorDiaria, int quantidadePortas)
        +CalcularAluguel(int dias) decimal
    }

    class CarroEletrico {
        -Bateria _bateria
        +CarroEletrico(int id, string placa, string modelo, decimal valorDiaria, int quantidadePortas)
        +CalcularAluguel(int dias) decimal
    }

    class Patinete {
        <<sealed>>
        -int _autonomiaKm
        -Bateria _bateria
        +int AutonomiaKm
        +Patinete(int id, string placa, string modelo, decimal valorDiaria, int autonomiaKm)
        +CalcularAluguel(int dias) decimal
    }

    class Bateria {
        <<internal>>
        +int ConsumoPorDia = 10
        +int NivelMinimoParaLocacao = 20
        +int Nivel
        +ConsumirParaLocacao(int dias)
        +Recarregar()
    }

    class Motorista {
        -string _nome
        -string _cpf
        -CNH _cnh
        +string Nome
        +string Cpf
        +CNH Cnh
        +GerarRelatorio() string
    }

    class CNH {
        -string _numero
        -CategoriaCnh _categoria
        -DateOnly _validade
        ~CNH(string numero, CategoriaCnh categoria, DateOnly validade)
    }

    class CategoriaCnh {
        <<enumeration>>
        A
        B
        AB
        C
        D
        E
    }

    class Frota {
        -string _nome
        -List~Veiculo~ _veiculos
        +Adicionar(Veiculo veiculo)
        +Remover(Veiculo veiculo) bool
        +Listar() IReadOnlyList~Veiculo~
        +GerarRelatorio() string
    }

    class Locacao {
        -Motorista _motorista
        -Veiculo _veiculo
        -int _dias
        +decimal ValorTotal
        +GerarRelatorio() string
    }

    class ConfiguradorSistema {
        <<static>>
        +decimal TaxaServico$
        +int TotalVeiculos$
        ~RegistrarVeiculoCriado()$
    }

    IImprimivel <|.. Veiculo
    IImprimivel <|.. Motorista
    IImprimivel <|.. Frota
    IImprimivel <|.. Locacao
    IEletrico <|.. Patinete
    IEletrico <|.. CarroEletrico

    Veiculo <|-- Carro
    Veiculo <|-- Patinete
    Carro <|-- CarroEletrico

    Patinete *-- Bateria
    CarroEletrico *-- Bateria
    Motorista *-- "1" CNH : composição
    CNH --> CategoriaCnh
    Frota o-- "0..*" Veiculo : agregação
    Locacao --> "1" Motorista : associação
    Locacao --> "1" Veiculo : associação
    Veiculo ..> ConfiguradorSistema
```

## Serviços e aplicação

```mermaid
classDiagram
    direction LR

    class Program {
        <<console>>
    }

    class VeiculoService {
        +CadastrarCarro(...) Carro
        +CadastrarCarroEletrico(...) CarroEletrico
        +CadastrarPatinete(...) Patinete
        +Listar() IReadOnlyList~Veiculo~
        +BuscarPorId(int id) Veiculo?
        +RealizarManutencao(int idVeiculo) string
        +RealizarManutencao(int idVeiculo, string pecaSubstituida) string
        +Recarregar(int idVeiculo) IEletrico
    }

    class MotoristaService {
        +Cadastrar(...) Motorista
        +Listar() IReadOnlyList~Motorista~
        +BuscarPorCpf(string cpf) Motorista?
    }

    class FrotaService {
        +Frota Frota
        +Adicionar(int idVeiculo) Veiculo
        +Remover(int idVeiculo) Veiculo
        +Listar() IReadOnlyList~Veiculo~
    }

    class LocacaoService {
        +Registrar(string cpf, int idVeiculo, int dias) Locacao
        +Listar() IReadOnlyList~Locacao~
    }

    class RelatorioService {
        +ListarImprimiveis() IReadOnlyList~IImprimivel~
        +GerarTodos() IReadOnlyList~string~
    }

    Program --> VeiculoService
    Program --> MotoristaService
    Program --> FrotaService
    Program --> LocacaoService
    Program --> RelatorioService
    FrotaService --> VeiculoService
    LocacaoService --> MotoristaService
    LocacaoService --> VeiculoService
    RelatorioService --> VeiculoService
    RelatorioService --> MotoristaService
    RelatorioService --> FrotaService
    RelatorioService --> LocacaoService
```

## Onde cada conceito de POO aparece

- **Encapsulamento:** todos os campos são privados, com propriedades públicas que validam no `set` (`Placa`, `ValorDiaria`, `Nome`) ou só têm `get` (`Id`, `Cpf`, `Cnh`).
- **Composição:** `Motorista` cria e possui a `CNH` (construtor `internal`); `Patinete` e `CarroEletrico` possuem uma `Bateria`.
- **Agregação:** `Frota` guarda referências a veículos que existem por conta própria.
- **Associação:** `Locacao` usa um `Motorista` e um `Veiculo` sem ser dona deles.
- **Membros estáticos:** `ConfiguradorSistema` guarda a taxa e o contador compartilhados por todo o sistema.
- **Herança:** `Carro` e `Patinete` herdam de `Veiculo`; `CarroEletrico` herda de `Carro`. Os construtores usam `: base(...)`.
- **Sobrescrita e polimorfismo:** `CalcularAluguel` é `virtual` em `Veiculo` e sobrescrito em cada tipo; `Locacao` e o menu chamam o método numa referência `Veiculo`.
- **Sobrecarga:** `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)`.
- **Classe selada:** `Patinete` é `sealed`.
- **Abstração:** `Veiculo` é `abstract`, com construtor `protected` e a propriedade abstrata `Tipo`.
- **Interfaces:** `IImprimivel` é implementada por classes sem relação de herança (`Veiculo`, `Motorista`, `Frota`, `Locacao`); `IEletrico` é implementada por `Patinete` e `CarroEletrico`, que têm bases diferentes.

## Regras de aluguel (taxa de 10%, 3 dias)

- **Carro** de R$ 150,00: 150 × 3 × 1,10 = 495,00 + seguro 20 × 3 = **R$ 555,00**
- **Carro elétrico** de R$ 150,00: 555,00 + recarga 10 × 3 = **R$ 585,00**
- **Patinete** de R$ 30,00: 30 × 3 × 1,10 = 99,00 × 0,80 = **R$ 79,20**
