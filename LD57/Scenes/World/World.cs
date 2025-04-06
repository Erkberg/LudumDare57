using Godot;
using System;

public partial class World : Node2D
{
    public static World inst;

    [Export] public Player player;
    [Export] public Vector2 bounds;

    public override void _EnterTree()
    {
        inst = this;
    }
}
