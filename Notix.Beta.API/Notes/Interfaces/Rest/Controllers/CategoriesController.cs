using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notix.Beta.API.Notes.Domain.Models;
using Notix.Beta.API.Notes.Domain.Services;
using Notix.Beta.API.Notes.Resources.Create;
using Notix.Beta.API.Notes.Resources.Show.Basic;
using Swashbuckle.AspNetCore.Annotations;

namespace Notix.Beta.API.Notes.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Controller for Categories - CRUD")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<BasicCategoryResource>> ListCategoriesAsync()
    {
        return _mapper.Map<IEnumerable<BasicCategoryResource>>(await _categoryService.ListAllAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryResource createCategoryResource)
    {
        var mappedResult = _mapper.Map<Category>(createCategoryResource);
        var result = await _categoryService.CreateAsync(mappedResult);
        if(!result.Success)
            return BadRequest(result.Message);
        return Ok(_mapper.Map<BasicCategoryResource>(result.Resource));
    }
}