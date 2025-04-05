using Godot;
using System;

public partial class Game : Node
{
    public static Game inst;

    [Export] public GameInput input;

    public override void _EnterTree()
    {
        inst = this;
    }
}
