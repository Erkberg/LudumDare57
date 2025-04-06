using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private HealthComponent health;
    [Export] private Shake2D spriteShake;

    public Vector2 moveDir = Vector2.Right;
    public float moveSpeed = 100f;
    public float damage = 1f;

    override public void _Ready()
    {
        health.Died += QueueFree;
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
    }

    private void Move(float delta)
    {
        GlobalPosition += moveDir * delta * moveSpeed;
    }

    public void DoDamage(float amount)
    {
        spriteShake.StartShake();
        health.Damage(amount);
    }
}
