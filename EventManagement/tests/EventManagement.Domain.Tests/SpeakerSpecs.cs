using System;
using Xunit;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Tests;

public class SpeakerSpecs
{
    [Fact]
    public void Construtor_ComDadosValidos_CriaPalestrante()
    {
        // Arrange & Act
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");

        // Assert
        Assert.Equal(1, palestrante.SpeakerId);
        Assert.Equal("João Silva", palestrante.FullName);
        Assert.Equal("joao@email.com", palestrante.Email);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_ComSpeakerIdInvalido_LancaExcecao(int idInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Speaker(idInvalido, "João Silva", "joao@email.com"));
        
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
            new Speaker(1, nomeInvalido!, "joao@email.com"));
        
        Assert.Contains("não pode ser nulo, vazio ou apenas espaços", excecao.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("email-invalido")]
    public void Construtor_ComEmailInvalido_LancaExcecao(string? emailInvalido)
    {
        // Act & Assert
        var excecao = Assert.Throws<ArgumentException>(() => 
            new Speaker(1, "João Silva", emailInvalido!));
        
        Assert.Contains("Email deve conter", excecao.Message);
    }

    [Fact]
    public void SetBiography_ComTextoValido_DefineBiografia()
    {
        // Arrange
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");
        var biografia = "Desenvolvedor experiente com 10+ anos em .NET";

        // Act
        palestrante.SetBiography(biografia);

        // Assert
        Assert.Equal(biografia, palestrante.Biography);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetBiography_ComTextoInvalido_DefineNull(string? biografiaInvalida)
    {
        // Arrange
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");

        // Act
        palestrante.SetBiography(biografiaInvalida);

        // Assert
        Assert.Null(palestrante.Biography);
    }

    [Fact]
    public void Company_ComNull_RetornaStringVazia()
    {
        // Arrange
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");

        // Act
        palestrante.Company = null;

        // Assert
        Assert.Equal(string.Empty, palestrante.Company);
    }

    [Fact]
    public void LinkedInProfile_ComNull_RetornaStringVazia()
    {
        // Arrange
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");

        // Act
        palestrante.LinkedInProfile = null;

        // Assert
        Assert.Equal(string.Empty, palestrante.LinkedInProfile);
    }

    [Fact]
    public void Equals_ComMesmoSpeakerId_RetornaTrue()
    {
        // Arrange
        var palestrante1 = new Speaker(1, "João Silva", "joao@email.com");
        var palestrante2 = new Speaker(1, "Maria Santos", "maria@email.com");

        // Act & Assert
        Assert.True(palestrante1.Equals(palestrante2));
    }

    [Fact]
    public void Equals_ComSpeakerIdDiferente_RetornaFalse()
    {
        // Arrange
        var palestrante1 = new Speaker(1, "João Silva", "joao@email.com");
        var palestrante2 = new Speaker(2, "João Silva", "joao@email.com");

        // Act & Assert
        Assert.False(palestrante1.Equals(palestrante2));
    }

    [Fact]
    public void GetHashCode_ComMesmoSpeakerId_RetornaMesmoHashCode()
    {
        // Arrange
        var palestrante1 = new Speaker(1, "João Silva", "joao@email.com");
        var palestrante2 = new Speaker(1, "Maria Santos", "maria@email.com");

        // Act & Assert
        Assert.Equal(palestrante1.GetHashCode(), palestrante2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ComSpeakerIdDiferente_RetornaHashCodeDiferente()
    {
        // Arrange
        var palestrante1 = new Speaker(1, "João Silva", "joao@email.com");
        var palestrante2 = new Speaker(2, "João Silva", "joao@email.com");

        // Act & Assert
        Assert.NotEqual(palestrante1.GetHashCode(), palestrante2.GetHashCode());
    }

    [Fact]
    public void ToString_RetornaStringFormatada()
    {
        // Arrange
        var palestrante = new Speaker(1, "João Silva", "joao@email.com");
        palestrante.Company = "Tech Corp";

        // Act
        var resultado = palestrante.ToString();

        // Assert
        Assert.Contains("João Silva", resultado);
        Assert.Contains("joao@email.com", resultado);
        Assert.Contains("Tech Corp", resultado);
    }

    [Fact]
    public void FullName_RemoveEspacosEmBranco()
    {
        // Arrange & Act
        var palestrante = new Speaker(1, "  João Silva  ", "joao@email.com");

        // Assert
        Assert.Equal("João Silva", palestrante.FullName);
    }

    [Fact]
    public void Email_RemoveEspacosEmBranco()
    {
        // Arrange & Act
        var palestrante = new Speaker(1, "João Silva", "  joao@email.com  ");

        // Assert
        Assert.Equal("joao@email.com", palestrante.Email);
    }
}