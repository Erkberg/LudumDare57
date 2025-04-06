using Godot;
using System;

public partial class Shake2D : Node2D
{
    [Export] private float duration = -1;
    [Export] private float magnitude = 0.4f;
    [Export] private Vector2 dimensions = Vector2.One;
    [Export] private bool diminishingMagnitude;
    [Export] private bool shakeOnReady;

    private Vector2 initialPosition;
    private bool isShaking;
    private float durationPassed;
    private float currentMagnitude;

    public override void _Ready()
    {
        initialPosition = Position;

        if (shakeOnReady)
        {
            StartShake();
        }
    }

    public override void _Process(double delta)
    {
        if (isShaking)
        {
            Shake(delta);
        }
    }

    public void StartShake()
    {
        currentMagnitude = magnitude;
        isShaking = true;
        durationPassed = 0;
    }

    public void StartShake(float duration, float magnitude)
    {
        this.duration = duration;
        this.magnitude = magnitude;

        StartShake();
    }

    public void CancelShake()
    {
        isShaking = false;
        Position = initialPosition;
    }

    private void Shake(double delta)
    {
        float shakeX = (float)GD.RandRange(-currentMagnitude, currentMagnitude) * dimensions.X;
        float shakeY = (float)GD.RandRange(-currentMagnitude, currentMagnitude) * dimensions.Y;

        Vector2 offset = new Vector2(shakeX, shakeY);
        Position = initialPosition + offset;

        durationPassed += (float)delta;
        if (duration > 0)
        {
            if (diminishingMagnitude)
            {
                currentMagnitude = magnitude * (1 - durationPassed / duration);
            }

            if (durationPassed > duration)
            {
                CancelShake();
            }
        }
    }
}