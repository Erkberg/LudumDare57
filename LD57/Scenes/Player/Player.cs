using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float posOffset = 100f;
    [Export] private float muzzleOffset = 25f;
    [Export] private Node2D muzzle;
    [Export] private Timer shootCooldownTimer;
    [Export] private PackedScene projectileScene;

    private GameInput input;

    public override void _Ready()
    {
        input = Game.inst.input;

        shootCooldownTimer.Timeout += OnShootCooldown;
    }

    public override void _Process(double delta)
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        Vector2 movement = input.GetMovement();
        Position = new Vector2(Mathf.RoundToInt(movement.X) * posOffset, Mathf.RoundToInt(movement.Y) * posOffset);
    }

    private void Shoot()
    {
        Vector2 shootDir = input.GetShootDir();
        if (shootDir == Vector2.Zero) return;
        muzzle.Position = shootDir * muzzleOffset;
    }

    private void OnShootCooldown()
    {
        Projectile projectile = projectileScene.Instantiate<Projectile>();
        projectile.GlobalPosition = muzzle.GlobalPosition;
        projectile.moveDir = muzzle.Position.Normalized();
        GetTree().Root.AddChild(projectile);
    }
}
