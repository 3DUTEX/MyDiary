using System.Text.Json.Serialization;
using Domain.Entities;

namespace Domain.DTOs.Notes;

public class CreateNoteDTO
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    [JsonIgnore]
    public int CreatedById { get; set; }

    public static explicit operator Note(CreateNoteDTO dto)
    {
        var note = new Note()
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedById = dto.CreatedById
        };

        return note;
    }
}