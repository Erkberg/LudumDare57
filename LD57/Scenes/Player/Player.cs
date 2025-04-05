using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float moveSpeed = 500f;
    [Export] private float jumpStrength = 400f;
    [Export] private float gravity = 10f;

    private GameInput input;

    public override void _Ready()
    {
        input = Game.inst.input;
    }

    public override void _Process(double delta)
    {
        CheckJump();
        Move();
    }

    private void CheckJump()
    {
        if (input.GetJumpDown())
        {
            Vector2 velo = Velocity;
            velo.Y = -jumpStrength;
            Velocity = velo;
        }
    }

    private void Move()
    {
        float movement = input.GetMovement();
        Vector2 velo = Velocity;
        velo.X = movement * moveSpeed;
        velo.Y += gravity;
        Velocity = velo;
        MoveAndSlide();
    }
}
