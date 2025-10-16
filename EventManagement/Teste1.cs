using EventManagement.Domain.Entities;
using EventManagement.Domain.Guards;

class TestesPalestrante {
    static void Main() {
        Console.WriteLine("=== TESTE 1: Validações do Palestrante ===");
        
        // ❌ Teste 1: ID zero (deve falhar)
        try {
            var teste1 = new Speaker(0, "João", "joao@email.com");
            Console.WriteLine("❌ ERRO: Deveria ter falhado!");
        } catch (Exception ex) {
            Console.WriteLine($"✅ ID zero: {ex.Message}");
        }
        
        // ❌ Teste 2: Nome vazio (deve falhar)
        try {
            var teste2 = new Speaker(1, "", "joao@email.com");
            Console.WriteLine("❌ ERRO: Deveria ter falhado!");
        } catch (Exception ex) {
            Console.WriteLine($"✅ Nome vazio: {ex.Message}");
        }
        
        // ❌ Teste 3: Email inválido (deve falhar)
        try {
            var teste3 = new Speaker(1, "João", "email-invalido");
            Console.WriteLine("❌ ERRO: Deveria ter falhado!");
        } catch (Exception ex) {
            Console.WriteLine($"✅ Email inválido: {ex.Message}");
        }
        
        // ✅ Teste 4: Dados válidos (deve funcionar)
        try {
            var teste4 = new Speaker(1, "João", "joao@email.com");
            Console.WriteLine("✅ Dados válidos: Palestrante criado com sucesso!");
        } catch (Exception ex) {
            Console.WriteLine($"❌ ERRO: {ex.Message}");
        }
    }
}
