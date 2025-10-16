# Desafio-Pratico-2025
Desafio que o professor passou na aula
 # 🎯 Event Management System

Um sistema completo de gerenciamento de eventos desenvolvido em C# seguindo princípios de Domain-Driven Design (DDD) e Clean Architecture, com foco em programação defensiva e null safety.

## 📋 Descrição

Sistema desenvolvido para uma empresa de organização de conferências, workshops e seminários. Permite o cadastro de eventos, palestrantes e locais com validações robustas e tratamento seguro de nullability.

## 🏗️ Arquitetura

```
EventManagement/
├── src/
│   ├── EventManagement.Domain/          # Camada de domínio
│   │   ├── Entities/                    # Entidades de negócio
│   │   │   ├── Speaker.cs              # Palestrante
│   │   │   ├── Venue.cs                # Local do evento
│   │   │   └── Event.cs                # Evento
│   │   └── Guards/                      # Validações
│   │       └── Guard.cs                # Classe Guard
│   └── EventManagement.Console/         # Aplicação de demonstração
│       └── Program.cs                  # Exemplos práticos
├── tests/
│   └── EventManagement.Domain.Tests/    # Testes unitários
│       ├── SpeakerSpecs.cs             # Testes Speaker
│       ├── VenueSpecs.cs               # Testes Venue
│       └── EventSpecs.cs               # Testes Event
└── docs/
    ├── README.md                       # Este arquivo
    └── EXPLICACAO.md                   # Documentação técnica
```

## 🚀 Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **C# 13** - Linguagem de programação
- **xUnit** - Framework de testes
- **Nullable Reference Types** - Para null safety
- **Guard Clauses Pattern** - Para validações defensivas

## 📦 Entidades do Domínio

### 🎤 Speaker (Palestrante)
- **Propriedades obrigatórias**: SpeakerId, FullName, Email
- **Propriedades opcionais**: Biography, Company, LinkedInProfile
- **Validações**: ID positivo, nome não vazio, email válido

### 🏛️ Venue (Local)
- **Propriedades obrigatórias**: VenueId, Name, Address, Capacity
- **Propriedades opcionais**: Description, ParkingInfo
- **Validações**: ID positivo, capacidade > 0, endereço válido

### 🎭 Event (Evento)
- **Propriedades obrigatórias**: EventId, Title, EventDate, Duration
- **Propriedades relacionadas**: Venue (lazy loading), MainSpeaker
- **Validações**: Data futura, duração mínima 30min

## 🛡️ Conceitos Aplicados

### ✅ Guard Clauses
Validações defensivas centralizadas na classe `Guard`:
```csharp
Guard.AgainstNegativeOrZero(speakerId, nameof(speakerId));
Guard.AgainstNullOrWhiteSpace(fullName, nameof(fullName));
Guard.AgainstPastDate(eventDate, nameof(eventDate));
```

### ✅ Null Safety
Uso estratégico de atributos de nullability:
- `[AllowNull]` - Permite null mas retorna string vazia
- `[DisallowNull]` - Impede atribuição de null
- `[NotNull]` - Garante não-null após construção

### ✅ Lazy Loading
Implementação de carregamento preguiçoso para propriedades opcionais:
```csharp
public Venue Venue
{
    get
    {
        if (_venue is null)
            _venue = Venue.Default;
        return _venue;
    }
}
```

### ✅ TryParseNonEmpty
Validação sem exceções para strings opcionais:
```csharp
if (Guard.TryParseNonEmpty(description, out string result))
{
    Description = result;
}
```

## ⚙️ Como Executar

### Pré-requisitos
- [.NET SDK 9.0+](https://dotnet.microsoft.com/download/dotnet/9.0)

### Passos

1. **Clone o repositório**
   ```bash
   git clone [url-do-repositorio]
   cd EventManagement
   ```

2. **Restaurar dependências**
   ```bash
   dotnet restore
   ```

3. **Executar testes**
   ```bash
   dotnet test
   ```

4. **Executar aplicação**
   ```bash
   dotnet run --project src/EventManagement.Console
   ```

## 🧪 Testes

O projeto inclui **45+ testes unitários** abrangentes:

### SpeakerSpecs (15 testes)
- ✅ Construtor com dados válidos
- ✅ Validações de SpeakerId negativo/zero
- ✅ Validações de FullName nulo/vazio
- ✅ Validações de Email inválido
- ✅ Testes de igualdade e hash code

### VenueSpecs (12 testes)
- ✅ Construtor com dados válidos
- ✅ Validações de capacidade
- ✅ Testes de Venue.Default
- ✅ Validações de endereço

### EventSpecs (18 testes)
- ✅ Construtor com dados válidos
- ✅ Validações de data passada
- ✅ Validações de duração mínima
- ✅ Testes de lazy loading
- ✅ Validações de EventCode

**Executar todos os testes:**
```bash
dotnet test --verbosity normal
```

## 📚 Exemplos de Uso

### Criando um Palestrante
```csharp
var speaker = new Speaker(1, "João Silva", "joao@email.com");
speaker.SetBiography("Especialista em C# com 10 anos de experiência");
speaker.Company = "Tech Corp";
```

### Criando um Local
```csharp
var venue = new Venue(1, "Centro de Convenções", "Av. Principal, 100", 500);
venue.SetDescription("Moderno centro com infraestrutura completa");
```

### Criando um Evento
```csharp
var evento = new Event(1, ".NET Conference", DateTime.Now.AddDays(30), TimeSpan.FromHours(8));
evento.SetEventCode("NETCONF2025");
evento.AssignMainSpeaker(speaker);
evento.AssignVenue(venue);
```

## 🎯 Funcionalidades Demonstradas

### Região 1: Speaker Examples
- Criação de palestrantes válidos
- Demonstração de validações com exceções
- Uso de `[AllowNull]` em Company e LinkedInProfile
- Método `SetBiography` com `TryParseNonEmpty`

### Região 2: Venue Examples  
- Criação de locais físicos e virtuais
- Propriedade estática `Venue.Default`
- Validações de capacidade e endereço

### Região 3: Event Examples
- Validações de data futura e duração
- Lazy loading do Venue
- Uso de `[DisallowNull]` em EventCode
- Atribuição de palestrante principal

### Região 4: Cenário Completo
- Integração de todas as entidades
- Demonstração de igualdade entre entidades
- Cenário realista de uso

## 🔧 Padrões de Projeto Implementados

1. **Guard Clauses** - Validações defensivas
2. **Lazy Loading** - Carregamento sob demanda
3. **Immutable Objects** - Objetos com estado controlado
4. **Null Object Pattern** - Retorno de valores padrão
5. **Factory Method** - Criação de objetos padrão

## 📊 Cobertura de Testes

- **100%** dos métodos públicos testados
- **Casos de sucesso e falha** cobertos
- **Valores boundary** testados
- **Exceções** validadas adequadamente

## 🚀 Próximos Passos

Possíveis extensões para o sistema:

1. **Persistência** - Adicionar repositórios e banco de dados
2. **API REST** - Expor funcionalidades via HTTP
3. **Validações Avançadas** - Regex para email, URLs
4. **Domain Events** - Para notificações e side effects
5. **Specification Pattern** - Para queries complexas


<div align="center">

**🚀 Desenvolvido com .NET 9.0 e C# 13**

</div>
