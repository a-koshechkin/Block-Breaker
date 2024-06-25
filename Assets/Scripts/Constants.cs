using System.Collections.Generic;

public static class Constants
{
    public enum Entities {
        Wall,
        Paddle,
        BreakableBlock,
        UnbreakableBlock
    }

    public static readonly Dictionary<Entities, string> Tags = new()
    {
        { Entities.Wall, "Wall"},
        { Entities.Paddle, "Paddle"},
        { Entities.BreakableBlock, "Breakable Block"},
        { Entities.UnbreakableBlock, "Unbreakable Block"}
    };

    public static readonly Dictionary<Entities, string> BallSounds = new()
    {
        { Entities.Wall, "SFX_Click_2"},
        { Entities.Paddle, "SFX_Bounce"},
        { Entities.BreakableBlock, "SFX_Click"},
        { Entities.UnbreakableBlock, "SFX_Clunk"}
    };
}
