using Domain.Entities;

namespace Domain.DTOs.Notes;

public class CreateNoteDTO
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public static explicit operator Note(CreateNoteDTO dto)
    {
        var note = new Note()
        {
            Title = dto.Title,
            Content = dto.Content
        };

        return note;
    }
}