using EventManagement.Domain.Entities;
using EventManagement.Domain.Guards;
using Xunit;

namespace EventManagement.Domain.Tests;

public class EventSpecs
{
    private readonly DateTime _dataFutura = DateTime.Now.AddDays(30);

    [Fact]
    public void Construtor_ComDadosValidos_CriaEvento()
    {
        // Arrange & Act
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Assert
        Assert.Equal(1, evento.EventId);
        Assert.Equal("Conferência de Tecnologia", evento.Title);
        Assert.Equal(_dataFutura, evento.EventDate);
        Assert.Equal(TimeSpan.FromHours(8), evento.Duration);
        Assert.Equal(string.Empty, evento.EventCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComEventIdInvalido_LancaExcecao(int idInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Event(idInvalido, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8)));
        
        Assert.Contains("deve ser maior que zero", excecao.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_ComTituloInvalido_LancaExcecao(string? tituloInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Event(1, tituloInvalido!, _dataFutura, TimeSpan.FromHours(8)));
        
        Assert.Contains("não pode ser nulo, vazio ou apenas espaços", excecao.Message);
    }

    [Fact]
    public void Construtor_ComDataPassada_LancaExcecao()
    {
        // Arrange
        var dataPassada = DateTime.Now.AddDays(-1);

        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Event(1, "Conferência de Tecnologia", dataPassada, TimeSpan.FromHours(8)));
        
        Assert.Contains("não pode ser no passado", excecao.Message);
    }

    [Fact]
    public void Construtor_ComDuracaoCurta_LancaExcecao()
    {
        // Arrange
        var duracaoCurta = TimeSpan.FromMinutes(29);

        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Event(1, "Conferência de Tecnologia", _dataFutura, duracaoCurta));
        
        Assert.Contains("Duração deve ser de pelo menos 30 minutos", excecao.Message);
    }

    [Fact]
    public void EventCode_InicializaComoStringVazia()
    {
        // Arrange & Act
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Assert
        Assert.Equal(string.Empty, evento.EventCode);
    }

    [Fact]
    public void SetEventCode_ComCodigoValido_DefineEventCode()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));
        var codigoEvento = "TECH2025";

        // Act
        evento.SetEventCode(codigoEvento);

        // Assert
        Assert.Equal(codigoEvento, evento.EventCode);
    }

    [Fact]
    public void SetEventCode_ComNull_LancaExcecao()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act & Assert
        var excecao = Assert.Throws<ArgumentNullException>(() => evento.SetEventCode(null!));
        Assert.Contains("não pode ser nulo", excecao.Message);
    }

    [Fact]
    public void SetEventCode_RemoveEspacosEmBranco()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        evento.SetEventCode("  TECH2025  ");

        // Assert
        Assert.Equal("TECH2025", evento.EventCode);
    }

    [Fact]
    public void EventCode_PropertySetter_FuncionaCorretamente()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        evento.EventCode = "TECH2025";

        // Assert
        Assert.Equal("TECH2025", evento.EventCode);
    }

    [Fact]
    public void SetDescription_ComTextoValido_DefineDescricao()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));
        var descricao = "Conferência anual para desenvolvedores";

        // Act
        evento.SetDescription(descricao);

        // Assert
        Assert.Equal(descricao, evento.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetDescription_ComTextoInvalido_DefineNull(string? descricaoInvalida)
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        evento.SetDescription(descricaoInvalida);

        // Assert
        Assert.Null(evento.Description);
    }

    [Fact]
    public void Requirements_ComNull_RetornaStringVazia()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        evento.Requirements = null;

        // Assert
        Assert.Equal(string.Empty, evento.Requirements);
    }

    [Fact]
    public void Notes_ComNull_RetornaStringVazia()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        evento.Notes = null;

        // Assert
        Assert.Equal(string.Empty, evento.Notes);
    }

    [Fact]
    public void Venue_UsaLazyLoading_RetornaPadrao()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        var local = evento.Venue;

        // Assert
        Assert.Equal("Evento Online", local.Name);
        Assert.Equal("Virtual", local.Address);
    }

    [Fact]
    public void Venue_MultiplosAcessos_RetornaMesmaInstancia()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act
        var local1 = evento.Venue;
        var local2 = evento.Venue;

        // Assert
        Assert.Same(local1, local2);
    }

    [Fact]
    public void AssignMainSpeaker_ComPalestranteValido_DefinePalestrantePrincipal()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");

        // Act
        evento.AssignMainSpeaker(palestrante);

        // Assert
        Assert.Equal(palestrante, evento.MainSpeaker);
    }

    [Fact]
    public void AssignMainSpeaker_ComNull_LancaExcecao()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act & Assert
        var excecao = Assert.Throws<ArgumentNullException>(() => evento.AssignMainSpeaker(null!));
        Assert.Contains("não pode ser nulo", excecao.Message);
    }

    [Fact]
    public void MainSpeaker_PodeSerNull()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));

        // Act & Assert
        Assert.Null(evento.MainSpeaker);
    }

    [Fact]
    public void ToString_RetornaStringFormatada()
    {
        // Arrange
        var evento = new Event(1, "Conferência de Tecnologia", _dataFutura, TimeSpan.FromHours(8));
        evento.SetEventCode("TECH2025");

        // Act
        var resultado = evento.ToString();

        // Assert
        Assert.Contains("Conferência de Tecnologia", resultado);
        Assert.Contains("TECH2025", resultado);
        Assert.Contains(_dataFutura.ToString("dd/MM/yyyy"), resultado);
    }
}