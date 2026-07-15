using SQLite;

namespace NotesPro.Models;

public class Note : BaseEntity
{
    [Indexed]
    public Guid CategoryId { get; set; }

    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string Description { get; set; } = string.Empty;

    public bool IsFavorite { get; set; }

    public bool IsPinned { get; set; }

    public bool IsDeleted { get; set; }
}