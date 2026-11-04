# Diagrama de classes: Sprint 2

Estado do código ao fim da Sprint 2. Membros com `-` são privados, com `+` são públicos e com `~` são `internal` (visíveis só dentro do projeto Models). Membros sublinhados (`$`) são estáticos.

## Modelos

```mermaid
classDiagram
    direction LR

    class Veiculo {
        -int _id
        -string _placa
        -string _modelo
        -decimal _valorDiaria
        +int Id
        +string Placa
        +string Modelo
        +decimal ValorDiaria
        +Veiculo(int id, string placa, string modelo, decimal valorDiaria)
    }

    class Motorista {
        -string _nome
        -string _cpf
        -CNH _cnh
        +string Nome
        +string Cpf
        +CNH Cnh
        +Motorista(string nome, string cpf, string numeroCnh, CategoriaCnh categoriaCnh, DateOnly validadeCnh)
    }

    class CNH {
        -string _numero
        -CategoriaCnh _categoria
        -DateOnly _validade
        +string Numero
        +CategoriaCnh Categoria
        +DateOnly Validade
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
        +string Nome
        +int Quantidade
        +Adicionar(Veiculo veiculo)
        +Remover(Veiculo veiculo) bool
        +Listar() IReadOnlyList~Veiculo~
    }

    class Locacao {
        -Motorista _motorista
        -Veiculo _veiculo
        -int _dias
        +Motorista Motorista
        +Veiculo Veiculo
        +int Dias
        +decimal ValorTotal
    }

    class ConfiguradorSistema {
        <<static>>
        -decimal _taxaServico$
        -int _totalVeiculos$
        +decimal TaxaServico$
        +int TotalVeiculos$
        ~RegistrarVeiculoCriado()$
    }

    Motorista *-- "1" CNH : composição
    CNH --> CategoriaCnh
    Frota o-- "0..*" Veiculo : agregação
    Locacao --> "1" Motorista : associação
    Locacao --> "1" Veiculo : associação
    Veiculo ..> ConfiguradorSistema : incrementa contador
    Locacao ..> ConfiguradorSistema : usa a taxa
```

## Serviços

```mermaid
classDiagram
    direction LR

    class VeiculoService {
        +Cadastrar(string placa, string modelo, decimal valorDiaria) Veiculo
        +Listar() IReadOnlyList~Veiculo~
        +BuscarPorId(int id) Veiculo?
    }

    class MotoristaService {
        +Cadastrar(string nome, string cpf, string numeroCnh, CategoriaCnh categoriaCnh, DateOnly validadeCnh) Motorista
        +Listar() IReadOnlyList~Motorista~
        +BuscarPorCpf(string cpf) Motorista?
    }

    class LocacaoService {
        +LocacaoService(MotoristaService motoristaService, VeiculoService veiculoService)
        +Registrar(string cpf, int idVeiculo, int dias) Locacao
        +Listar() IReadOnlyList~Locacao~
    }

    class FrotaService {
        +FrotaService(Frota frota, VeiculoService veiculoService)
        +Frota Frota
        +Adicionar(int idVeiculo) Veiculo
        +Remover(int idVeiculo) Veiculo
        +Listar() IReadOnlyList~Veiculo~
    }

    LocacaoService --> MotoristaService
    LocacaoService --> VeiculoService
    FrotaService --> VeiculoService
```

## Os três relacionamentos

- **Composição (`Motorista` e `CNH`, losango preenchido):** a CNH é criada dentro do construtor do `Motorista` e o construtor da `CNH` é `internal`, então ela não existe sem o motorista e não pode ser trocada.
- **Agregação (`Frota` e `Veiculo`, losango vazio):** a frota guarda referências a veículos que existem por conta própria. Remover da frota não apaga o veículo, e o mesmo veículo pode estar em mais de uma frota.
- **Associação (`Locacao` com `Motorista` e `Veiculo`, seta):** a locação só referencia um motorista e um veículo já existentes para calcular o valor, sem ser dona deles.

## Membros estáticos

- `ConfiguradorSistema` é uma classe `static`: existe um único valor de `TaxaServico` e de `TotalVeiculos` para todo o sistema.
- O construtor de `Veiculo` chama `RegistrarVeiculoCriado()` depois das validações, e o `VeiculoService` usa `TotalVeiculos` para gerar o próximo Id.
- `Locacao.ValorTotal` lê a taxa no momento do cálculo, então alterar a taxa muda o valor das locações.
