using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Speaker
{
    public int SpeakerId { get; }
    public string FullName { get; }
    public string Email { get; }
    
    private string? _biography;
    private string? _company;
    private string? _linkedInProfile;

    public Speaker(int speakerId, string fullName, string email)
    {
        Guard.AgainstNegativeOrZero(speakerId, nameof(speakerId));
        Guard.AgainstNullOrWhiteSpace(fullName, nameof(fullName));
        Guard.AgainstNullOrWhiteSpace(email, nameof(email));
        
        if (!Guard.IsValidEmail(email))
            throw new ArgumentException("Email deve conter o caractere '@'.", nameof(email));

        SpeakerId = speakerId;
        FullName = fullName.Trim();
        Email = email.Trim();
    }

    public string? Biography 
    { 
        get => _biography;
        private set => _biography = value;
    }

    [AllowNull]
    public string Company
    {
        get => _company ?? string.Empty;
        set => _company = value;
    }

    [AllowNull]
    public string LinkedInProfile
    {
        get => _linkedInProfile ?? string.Empty;
        set => _linkedInProfile = value;
    }

    public void SetBiography(string? biography)
    {
        if (Guard.TryParseNonEmpty(biography, out string result))
        {
            Biography = result;
        }
        else
        {
            Biography = null;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Speaker speaker && SpeakerId == speaker.SpeakerId;
    }

    public override int GetHashCode()
    {
        return SpeakerId.GetHashCode();
    }

    public override string ToString()
    {
        return $"{FullName} ({Email}) - {Company}";
    }
}