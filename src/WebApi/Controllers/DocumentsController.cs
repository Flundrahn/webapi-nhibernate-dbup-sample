using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Documents;

namespace WebApi.Controllers;

[ApiController]
[Route("documents")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentRepository _repository;

    public DocumentsController(IDocumentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<Ok<ICollection<Document>>> GetAll()
    {
        var documents = await _repository.GetAll();
        return TypedResults.Ok(documents);
    }

    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<Document>>> Get(int id)
    {
        var document = await _repository.Get(id);
        return document is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(document);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDocumentRequest request)
    {
        if (request.Name.Length > 255)
        {
            return BadRequest("Name can be maximum 255 characters");
        }

        Document document = new();
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            document.Name = request.Name;
        }

        await _repository.Add(document);
        return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
    }
}
