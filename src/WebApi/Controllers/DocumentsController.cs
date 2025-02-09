using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace WebApi.Controllers;

[ApiController]
[Route("documents")]
public class DocumentsController : ControllerBase
{
    private readonly NHibernate.ISession _dbSession;

    public DocumentsController(NHibernate.ISession dbSession)
    {
        _dbSession = dbSession;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var documents = await _dbSession.Query<Document>().ToListAsync();
        return Ok(documents);
    }
}
