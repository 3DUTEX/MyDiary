namespace Domain.Entities;

public class Note
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedIn { get; set; } = DateTime.Now;
}