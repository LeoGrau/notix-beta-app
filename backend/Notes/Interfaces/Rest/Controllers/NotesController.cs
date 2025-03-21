using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Repositories;
using Notix.Beta.API.Notes.Domain.Services;
using Notix.Beta.API.Notes.Resources.Create;
using Notix.Beta.API.Notes.Resources.Show.Basic;
using Notix.Beta.API.Notes.Resources.Show.Detailed;
using Notix.Beta.API.Notes.Resources.Update;
using Notix.Beta.API.Notes.Resources.Update.Patch;
using Swashbuckle.AspNetCore.Annotations;

namespace Notix.Beta.API.Notes.Interfaces.Rest.Controllers;

[Authorize]
[ApiController]
[Route("api/v0/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Controller for Notes - CRUD")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;
    private readonly IMapper _mapper;

    public NotesController(INoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<BasicNoteResource>> ListNotesAsync()
    {
        return _mapper.Map<IEnumerable<BasicNoteResource>>(await _noteService.ListAllAsync());
    }
    
    [HttpGet("list/{userId:int}")]
    public async Task<IEnumerable<BasicNoteResource>> ListNotesByUserIdAsync(int userId)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrEmpty(claimUserId) || claimUserId != userId.ToString())
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
        Console.WriteLine($"claimUserId: {claimUserId}");
        return _mapper.Map<IEnumerable<BasicNoteResource>>(await _noteService.ListByUserIdAsync(userId));
    }
    
    [HttpGet("status/{userId:int}")]
    public async Task<IEnumerable<BasicNoteResource>> ListNotesByUserIdAndIsActiveStatusAsync(
        [FromQuery(Name = "is_archive")] bool isArchive, int userId)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrEmpty(claimUserId) || claimUserId != userId.ToString())
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
        return _mapper.Map<IEnumerable<BasicNoteResource>>(await _noteService.ListNotesByUserIdAndIsActiveStatusAsync(isArchive, userId));
    }
        
    [HttpGet("status")]
    public async Task<IEnumerable<BasicNoteResource>> ListNotesByIsActiveStatusAsync(
        [FromQuery(Name = "is_archive")] bool isArchive)
    {
        return _mapper.Map<IEnumerable<BasicNoteResource>>(await _noteService.ListByIsArchivedStatusAsync(isArchive));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> FindNoteAsync([FromRoute] int id)
    {
        var result = await _noteService.FindAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<DetailedNoteResource>(result.Resource));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNoteAsync([FromBody] CreateNoteResource resource)
    {
        var mappedResource = _mapper.Map<CreateNoteResource, Note>(resource);
        var result = await _noteService.CreateAsync(mappedResource);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<DetailedNoteResource>(result.Resource));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateNoteAsync([FromRoute] int id, [FromBody] UpdateNoteResource resource)
    {
        var mappedResource = _mapper.Map<UpdateNoteResource, Note>(resource);
        var result = await _noteService.UpdateAsync(id, mappedResource);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<DetailedNoteResource>(result.Resource));
    }

    [HttpPatch("archive/{id:int}")]
    public async Task<IActionResult> SetIsArchiveStatusAsync([FromRoute] int id, [FromBody] bool isArchive)
    {
        var result = await _noteService.SetIsArchiveStatusAsync(id, isArchive);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<DetailedNoteResource>(result.Resource));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNoteAsync([FromRoute] int id)
    {
        var result = await _noteService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<DetailedNoteResource>(result.Resource));
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IEnumerable<BasicNoteResource>> ListByCategoryIdAsync([FromRoute] int categoryId,
        [FromQuery(Name = "is_archived")] bool isArchived)
    {
        return _mapper.Map<IEnumerable<BasicNoteResource>>(
            await _noteService.ListByCategoryAndIsArchivedAsync(categoryId, isArchived));
    }
    
    [HttpGet("{userId:int}/category/{categoryId:int}")]
    public async Task<IEnumerable<BasicNoteResource>> ListByCategoryIdAndUserIdAsync([FromRoute] int categoryId,
        [FromQuery(Name = "is_archived")] bool isArchived, int userId)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrEmpty(claimUserId) || claimUserId != userId.ToString())
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
        return _mapper.Map<IEnumerable<BasicNoteResource>>(
            await _noteService.ListByCategoryAndIsArchivedAndUserIdAsync(categoryId, isArchived, userId));
    }
}