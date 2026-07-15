using SQLite;

namespace NotesPro.Models;

public abstract class BaseEntity
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }
}