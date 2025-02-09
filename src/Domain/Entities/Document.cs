namespace Domain.Entities;

public class Document
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = "New document";
    public virtual DateTimeOffset CreateDate { get; protected set; }

    public Document()
    {
        CreateDate = DateTimeOffset.Now;
    }
}
