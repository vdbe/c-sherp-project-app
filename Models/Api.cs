using SQLite;

namespace c_sherp_project_app.Models;

[Table("game_identifier")]
public class GameIdentifier
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public Guid Guid { get; set; }
}