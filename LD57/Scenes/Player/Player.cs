using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float posOffset = 100f;
    [Export] private float muzzleOffset = 25f;
    [Export] private Node2D muzzle;

    private GameInput input;

    public override void _Ready()
    {
        input = Game.inst.input;
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
        muzzle.Position = shootDir * muzzleOffset;
    }
}
