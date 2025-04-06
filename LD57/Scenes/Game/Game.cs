using Godot;
using System;

public partial class Game : Node
{
    public static Game inst;

    [Export] public GameInput input;
    [Export] public GameData data;
    [Export] public GameState state;

    public override void _EnterTree()
    {
        inst = this;
    }

    public void SetPaused(bool paused)
    {
        GetTree().Paused = paused;
    }
}
