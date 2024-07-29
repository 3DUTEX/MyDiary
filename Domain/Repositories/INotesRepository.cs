using Domain.Entities;
using Domain.Repositories.Configs;

namespace Domain.Repositories;

public interface INotesRepository
{
    Task<Note?> LoadById(string id);
    Task<string> Create(Note note);
    Task<IEnumerable<Note>> Load(PaginationConfig? config = null);
    Task<string> DeleteById(string id);
}