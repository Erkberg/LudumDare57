using Godot;
using System;

public partial class World : Node2D
{
    public static World inst;

    [Export] public Player player;

    public override void _EnterTree()
    {
        inst = this;
    }
}
