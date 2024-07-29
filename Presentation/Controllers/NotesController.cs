using Domain.DTOs.Notes;
using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INotesRepository _notesRepository;

    public NotesController(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Load([FromQuery] PaginationConfig? paginationConfig = null)
    {
        return Ok(await _notesRepository.Load(paginationConfig));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> LoadById([FromRoute] string id)
    {
        return Ok(await _notesRepository.LoadById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteDTO dto)
    {
        return Ok(await _notesRepository.Create((Note)dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] string id)
    {
        return Ok(await _notesRepository.DeleteById(id));
    }
}