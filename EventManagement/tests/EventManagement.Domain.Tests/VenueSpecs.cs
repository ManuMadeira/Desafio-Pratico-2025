using EventManagement.Domain.Entities;
using EventManagement.Domain.Guards;
using Xunit;

namespace EventManagement.Domain.Tests;

public class VenueSpecs
{
    [Fact]
    public void Construtor_ComDadosValidos_CriaLocal()
    {
        // Arrange & Act
        var local = new Venue(1, "Centro de Convenções", "Rua Principal, 123", 500);

        // Assert
        Assert.Equal(1, local.VenueId);
        Assert.Equal("Centro de Convenções", local.Name);
        Assert.Equal("Rua Principal, 123", local.Address);
        Assert.Equal(500, local.Capacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComVenueIdInvalido_LancaExcecao(int idInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Venue(idInvalido, "Centro de Convenções", "Rua Principal, 123", 500));
        
        Assert.Contains("deve ser maior que zero", excecao.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_ComNomeInvalido_LancaExcecao(string? nomeInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Venue(1, nomeInvalido!, "Rua Principal, 123", 500));
        
        Assert.Contains("não pode ser nulo, vazio ou apenas espaços", excecao.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_ComEnderecoInvalido_LancaExcecao(string? enderecoInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Venue(1, "Centro de Convenções", enderecoInvalido!, 500));
        
        Assert.Contains("não pode ser nulo, vazio ou apenas espaços", excecao.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComCapacidadeInvalida_LancaExcecao(int capacidadeInvalida)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Venue(1, "Centro de Convenções", "Rua Principal, 123", capacidadeInvalida));
        
        Assert.Contains("deve ser maior que zero", excecao.Message);
    }

    [Fact]
    public void SetDescription_ComTextoValido_DefineDescricao()
    {
        // Arrange
        var local = new Venue(1, "Centro de Convenções", "Rua Principal, 123", 500);
        var descricao = "Local moderno com infraestrutura completa";

        // Act
        local.SetDescription(descricao);

        // Assert
        Assert.Equal(descricao, local.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetDescription_ComTextoInvalido_DefineNull(string? descricaoInvalida)
    {
        // Arrange
        var local = new Venue(1, "Centro de Convenções", "Rua Principal, 123", 500);

        // Act
        local.SetDescription(descricaoInvalida);

        // Assert
        Assert.Null(local.Description);
    }

    [Fact]
    public void ParkingInfo_ComNull_RetornaStringVazia()
    {
        // Arrange
        var local = new Venue(1, "Centro de Convenções", "Rua Principal, 123", 500);

        // Act
        local.ParkingInfo = null;

        // Assert
        Assert.Equal(string.Empty, local.ParkingInfo);
    }

    [Fact]
    public void Default_Property_RetornaEventoOnline()
    {
        // Act
        var localPadrao = Venue.Default;

        // Assert
        Assert.Equal("Evento Online", localPadrao.Name);
        Assert.Equal("Virtual", localPadrao.Address);
        Assert.Equal(1000, localPadrao.Capacity);
    }

    [Fact]
    public void Equals_ComMesmoVenueId_RetornaTrue()
    {
        // Arrange
        var local1 = new Venue(1, "Local A", "Endereço A", 100);
        var local2 = new Venue(1, "Local B", "Endereço B", 200);

        // Act & Assert
        Assert.True(local1.Equals(local2));
    }

    [Fact]
    public void Equals_ComVenueIdDiferente_RetornaFalse()
    {
        // Arrange
        var local1 = new Venue(1, "Local A", "Endereço A", 100);
        var local2 = new Venue(2, "Local A", "Endereço A", 100);

        // Act & Assert
        Assert.False(local1.Equals(local2));
    }

    [Fact]
    public void GetHashCode_ComMesmoVenueId_RetornaMesmoHashCode()
    {
        // Arrange
        var local1 = new Venue(1, "Local A", "Endereço A", 100);
        var local2 = new Venue(1, "Local B", "Endereço B", 200);

        // Act & Assert
        Assert.Equal(local1.GetHashCode(), local2.GetHashCode());
    }

    [Fact]
    public void ToString_RetornaStringFormatada()
    {
        // Arrange
        var local = new Venue(1, "Centro de Convenções", "Rua Principal, 123", 500);

        // Act
        var resultado = local.ToString();

        // Assert
        Assert.Contains("Centro de Convenções", resultado);
        Assert.Contains("Rua Principal, 123", resultado);
        Assert.Contains("500", resultado);
    }
}