namespace NotesPro.Data.Database;

public static class DatabaseConstants
{
    public const string DatabaseName = "notespro.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
}