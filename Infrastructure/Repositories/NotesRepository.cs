using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Configs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotesRepository : INotesRepository
{
    AppDbContext _context;

    public NotesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Note?> LoadById(string id)
    {
        var note = await _context.Notes.FindAsync(id);

        return note;
    }

    public async Task<string> Create(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return note.Id;
    }

    public async Task<IEnumerable<Note>> Load(PaginationConfig? paginationConfig = null)
    {
        var notes = await _context.Notes
            .OrderBy(n => n.CreatedIn)
            .Skip(paginationConfig?.Skip ?? 0)
            .Take(paginationConfig?.Take ?? 20)
            .ToListAsync();

        return notes ?? [];
    }

    public async Task<string> DeleteById(string id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note is not null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        return id;
    }
}