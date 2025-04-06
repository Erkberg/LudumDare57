using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private HealthComponent health;
    [Export] private Shake2D spriteShake;

    public Vector2 moveDir = Vector2.Right;
    public float moveSpeed = 100f;
    public float damage = 1f;

    private float rotateSpeed;

    override public void _Ready()
    {
        health.Died += QueueFree;

        rotateSpeed = (float)GD.RandRange(-1f, 1f);
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
        RotateSprite((float)delta);
        CheckDestroy();
    }

    private void Move(float delta)
    {
        GlobalPosition += moveDir * delta * moveSpeed;
    }

    private void RotateSprite(float delta)
    {
        spriteShake.Rotate(rotateSpeed * delta);
    }

    public void DoDamage(float amount)
    {
        spriteShake.StartShake();
        health.Damage(amount);
    }

    private void CheckDestroy()
    {
        if (World.inst.IsOusideBounds(GlobalPosition))
        {
            //GD.Print($"Out of bounds enemy at {GlobalPosition} with dir {moveDir}");
            QueueFree();
        }
    }
}
