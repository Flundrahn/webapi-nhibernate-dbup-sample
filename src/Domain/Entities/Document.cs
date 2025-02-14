namespace Domain.Entities;

public class Document : Entity
{
    public virtual string Name { get; set; } = "New document";
    public virtual DateTimeOffset CreateDate { get; protected set; }

    public Document()
    {
        CreateDate = DateTimeOffset.Now;
    }
}
