using Godot;
using System;

public partial class Projectile : Area2D
{
    public Vector2 moveDir;
    public float moveSpeed = 100f;
    public float damage = 1f;

    override public void _Ready()
    {
        AreaEntered += OnAreaEntered;
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
        if (GlobalPosition.X < 0 || GlobalPosition.X > World.inst.bounds.X || GlobalPosition.Y < 0 || GlobalPosition.Y > World.inst.bounds.Y)
        {
            QueueFree();
        }
    }
}
