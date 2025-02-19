using Domain.Entities;

namespace WebApi.Models.Documents;

public class CreateDocumentRequest
{
    public string Name { get; set; } = string.Empty;
}
