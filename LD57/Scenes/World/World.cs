using Godot;
using System;

public partial class World : Node2D
{
    public static World inst;

    [Export] public Player player;
    [Export] public IngameUI ui;
    [Export] public Vector2 bounds;
    [Export] public Vector2 spawnOffset;
    [Export] public float gridOffset = 100f;

    public override void _EnterTree()
    {
        inst = this;
    }

    public Vector2 GetCenter()
    {
        return bounds / 2f;
    }

    public bool IsOusideBounds(Vector2 pos)
    {
        return pos.X < 0 - spawnOffset.X || pos.X > bounds.X + spawnOffset.X || pos.Y < 0 - spawnOffset.Y || pos.Y > bounds.Y + spawnOffset.Y;
    }
}
