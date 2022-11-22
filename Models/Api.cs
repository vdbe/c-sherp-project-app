using SQLite;

namespace c_sherp_project_app.Models;

[Table("api_identifier")]
public class ApiIdentifier
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250), Unique]
    public string Identifier { get; set; }
}