using Godot;
using System;

public partial class Projectile : Area2D
{
    public Vector2 moveDir;
    public float moveSpeed = 200f;
    public float damage = 1f;

    private const string ProjectileName = "Projectile";

    override public void _Ready()
    {
        AreaEntered += OnAreaEntered;
        Name = ProjectileName;
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
        CheckDestroy();
    }

    private void Move(float delta)
    {
        GlobalPosition += moveDir * delta * moveSpeed;
    }

    private void OnAreaEntered(Area2D other)
    {
        if (other is Enemy enemy)
        {
            enemy.DoDamage(damage);
            QueueFree();
        }
    }

    private void CheckDestroy()
    {
        if (World.inst.IsOusideBounds(GlobalPosition))
        {
            QueueFree();
        }
    }
}
