using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll()
    {
        var documents = await _repository.GetAll();
        return Ok(documents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var document = await _repository.Get(id);
        return document is null
            ? NotFound()
            : Ok(document);
    }
}
