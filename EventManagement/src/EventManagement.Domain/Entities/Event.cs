using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Event
{
    public int EventId { get; }
    public string Title { get; }
    public DateTime EventDate { get; }
    public TimeSpan Duration { get; }
    
    private string _eventCode = string.Empty;
    private string? _description;
    private string? _requirements;
    private string? _notes;
    private Venue? _venue;
    private Speaker? _mainSpeaker;

    public Event(int eventId, string title, DateTime eventDate, TimeSpan duration)
    {
        Guard.AgainstNegativeOrZero(eventId, nameof(eventId));
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Guard.AgainstPastDate(eventDate, nameof(eventDate));
        
        if (duration < TimeSpan.FromMinutes(30))
            throw new ArgumentException("Duração deve ser de pelo menos 30 minutos.", nameof(duration));

        EventId = eventId;
        Title = title.Trim();
        EventDate = eventDate;
        Duration = duration;
    }

    [DisallowNull]
    public string EventCode
    {
        get => _eventCode;
        set => SetEventCode(value);
    }

    public string? Description
    {
        get => _description;
        private set => _description = value;
    }

    [AllowNull]
    public string Requirements
    {
        get => _requirements ?? string.Empty;
        set => _requirements = value;
    }

    [AllowNull]
    public string Notes
    {
        get => _notes ?? string.Empty;
        set => _notes = value;
    }

    public Venue Venue
    {
        get
        {
            if (_venue is null)
            {
                _venue = Venue.Default;
            }
            return _venue;
        }
        private set => _venue = value;
    }

    public Speaker? MainSpeaker
    {
        get => _mainSpeaker;
        private set => _mainSpeaker = value;
    }

    public void SetEventCode(string code)
    {
        Guard.AgainstNull(code, nameof(code));
        _eventCode = code.Trim();
    }

    public void SetDescription(string? description)
    {
        if (Guard.TryParseNonEmpty(description, out string result))
        {
            Description = result;
        }
        else
        {
            Description = null;
        }
    }

    public void AssignMainSpeaker(Speaker speaker)
    {
        Guard.AgainstNull(speaker, nameof(speaker));
        MainSpeaker = speaker;
    }

    public void AssignVenue(Venue venue)
    {
        Guard.AgainstNull(venue, nameof(venue));
        Venue = venue;
    }

    public override string ToString()
    {
        return $"{Title} ({EventCode}) - {EventDate:dd/MM/yyyy HH:mm} - Duração: {Duration:h\\:mm} - Local: {Venue.Name}";
    }
}