using System.Diagnostics.CodeAnalysis;

namespace EventManagement.Domain.Guards;

public static class Guard
{
    public static void AgainstNull<T>([NotNull] T? value, string paramName)
        where T : class
    {
        if (value is null)
            throw new ArgumentNullException(paramName, $"{paramName} não pode ser nulo.");
    }

    public static void AgainstNullOrEmpty([NotNull] string? value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException($"{paramName} não pode ser nulo ou vazio.", paramName);
    }

    public static void AgainstNullOrWhiteSpace([NotNull] string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} não pode ser nulo, vazio ou apenas espaços.", paramName);
    }

    public static void AgainstNegativeOrZero(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, 
                $"{paramName} deve ser maior que zero.");
    }

    public static void AgainstPastDate(DateTime date, string paramName)
    {
        if (date < DateTime.Now)
            throw new ArgumentException(
                $"{paramName} não pode ser no passado.", paramName);
    }

    public static bool IsValidEmail(string? email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains('@');
    }

    public static bool TryParseNonEmpty(string? input, out string result)
    {
        result = string.Empty;
        
        if (string.IsNullOrWhiteSpace(input))
            return false;
            
        result = input.Trim();
        return true;
    }
}