using EventManagement.Domain.Entities;
using EventManagement.Domain.Guards;

namespace EventManagement.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("=== Sistema de Gerenciamento de Eventos ===");
        System.Console.WriteLine();

        #region 1: Exemplos de Palestrantes
        System.Console.WriteLine("🎤 REGIÃO 1: EXEMPLOS DE PALESTRANTES");
        System.Console.WriteLine("======================================");

        try
        {
            // Criar palestrantes válidos
            var speaker1 = new Speaker(1, "João Silva", "joao.silva@email.com");
            speaker1.SetBiography("Especialista em C# com 10 anos de experiência");
            speaker1.Company = "Microsoft";
            speaker1.LinkedInProfile = "https://linkedin.com/in/joaosilva";

            var speaker2 = new Speaker(2, "Maria Santos", "maria.santos@tech.com");
            speaker2.SetBiography("Arquiteta de Software e consultora em DevOps");

            System.Console.WriteLine("✅ Palestrantes criados com sucesso:");
            System.Console.WriteLine($"   - {speaker1}");
            System.Console.WriteLine($"   - {speaker2}");
            System.Console.WriteLine();

            // Demonstrar Company e LinkedInProfile com null
            System.Console.WriteLine("📊 Propriedades com [AllowNull]:");
            System.Console.WriteLine($"   Empresa do João: '{speaker1.Company}'");
            System.Console.WriteLine($"   LinkedIn do João: '{speaker1.LinkedInProfile}'");
            System.Console.WriteLine($"   Empresa da Maria: '{speaker2.Company}'");
            System.Console.WriteLine($"   LinkedIn da Maria: '{speaker2.LinkedInProfile}'");
            System.Console.WriteLine();

            // Demonstrar SetBiography com diferentes valores
            System.Console.WriteLine("📝 Testando SetBiography:");
            speaker1.SetBiography("   Nova biografia com espaços   ");
            System.Console.WriteLine($"   Biografia com trim: '{speaker1.Biography}'");
            
            speaker1.SetBiography(null);
            System.Console.WriteLine($"   Biografia com null: '{speaker1.Biography ?? "null"}'");
            
            speaker1.SetBiography("   ");
            System.Console.WriteLine($"   Biografia com espaços: '{speaker1.Biography ?? "null"}'");
            System.Console.WriteLine();

            // Tentar criar com dados inválidos
            System.Console.WriteLine("❌ Tentativas de criação inválidas:");
            try
            {
                var invalidSpeaker = new Speaker(0, "Nome Válido", "email@valido.com");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ SpeakerId zero: {ex.Message}");
            }

            try
            {
                var invalidSpeaker = new Speaker(1, "   ", "email@valido.com");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ Nome vazio: {ex.Message}");
            }

            try
            {
                var invalidSpeaker = new Speaker(1, "Nome Válido", "email-invalido");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ Email inválido: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
        }
        
        System.Console.WriteLine();
        #endregion
        
        #region 2: Exemplos de Locais
        System.Console.WriteLine("🏛️  REGIÃO 2: EXEMPLOS DE LOCAIS");
        System.Console.WriteLine("================================");

        try
        {
            // Criar locais válidos
            var venue1 = new Venue(1, "Centro de Convenções", "Avenida Principal, 1000", 500);
            venue1.SetDescription("   Moderno centro com infraestrutura completa   ");
            venue1.ParkingInfo = "Estacionamento subterrâneo disponível";

            var venue2 = new Venue(2, "Auditório Municipal", "Rua das Flores, 500", 200);

            System.Console.WriteLine("✅ Locais criados com sucesso:");
            System.Console.WriteLine($"   - {venue1}");
            System.Console.WriteLine($"   - {venue2}");
            System.Console.WriteLine();

            // Demonstrar Venue.Default
            System.Console.WriteLine("🌐 Venue.Default (Local Virtual):");
            var defaultVenue = Venue.Default;
            System.Console.WriteLine($"   - {defaultVenue}");
            System.Console.WriteLine();

            // Mostrar SetDescription
            System.Console.WriteLine("📝 Testando SetDescription:");
            venue1.SetDescription("Descrição atualizada do local");
            System.Console.WriteLine($"   Descrição válida: '{venue1.Description}'");
            
            venue1.SetDescription(null);
            System.Console.WriteLine($"   Descrição com null: '{venue1.Description ?? "null"}'");
            System.Console.WriteLine();

            // Demonstrar ParkingInfo com [AllowNull]
            System.Console.WriteLine("🅿️  ParkingInfo com [AllowNull]:");
            System.Console.WriteLine($"   Estacionamento venue1: '{venue1.ParkingInfo}'");
            System.Console.WriteLine($"   Estacionamento venue2: '{venue2.ParkingInfo}'");
            
            venue2.ParkingInfo = null;
            System.Console.WriteLine($"   Estacionamento venue2 após null: '{venue2.ParkingInfo}'");
            System.Console.WriteLine();

            // Tentar criar com dados inválidos
            System.Console.WriteLine("❌ Tentativas de criação inválidas:");
            try
            {
                var invalidVenue = new Venue(0, "Nome Válido", "Endereço Válido", 100);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ VenueId zero: {ex.Message}");
            }

            try
            {
                var invalidVenue = new Venue(1, "   ", "Endereço Válido", 100);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ Nome vazio: {ex.Message}");
            }

            try
            {
                var invalidVenue = new Venue(1, "Nome Válido", "Endereço Válido", 0);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"   ❌ Capacidade zero: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
        }

        System.Console.WriteLine();
        #endregion
    }
}