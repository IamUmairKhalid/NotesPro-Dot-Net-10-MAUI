using SQLite;

namespace NotesPro.Models;

public class Category : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Color { get; set; } = "#6750A4";

    [MaxLength(50)]
    public string Icon { get; set; } = "folder";
}