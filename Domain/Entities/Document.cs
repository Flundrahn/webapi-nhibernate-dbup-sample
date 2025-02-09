namespace Domain.Entities;

public class Document
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual DateTimeOffset CreateDate { get; protected set; }

    public Document(string name)
    {
        Name = name;
        CreateDate = DateTimeOffset.Now;
    }
}
