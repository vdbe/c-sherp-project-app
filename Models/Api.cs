using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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

public class Game
{
    public Guid Guid { get; set; }
    public bool Active { get; set; }
    public uint Score { get; set; }
}
public enum Choice
{
    [Description("Rock")]
    Rock = 0,
    [Description("Paper")]
    Paper = 1,
    [Description("Scissors")]
    Scissors = 2,
}

public enum Result
{
    [Description("Draw")]
    Draw = 0,
    [Description("Win")]
    Win = 1,
    [Description("Loss")]
    Loss = 2,

}
public class LeaderBoard
{
    public string Name { get; set; }

    public DateTime On { get; set; }

    public Game Game { get; set; }

}
public class Play
{
    [Required]
    public Choice Choice { get; set; }

}
public class PlayResult
{
    public Game Game { get; set; }

    public Choice Choice { get; set; }

    public Result Result { get; set; }
}

public class LeaderBoardSubmission
{
    public string Name { get; set;}
    public Guid Game { get; set; }
}