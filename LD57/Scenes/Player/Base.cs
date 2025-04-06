using Godot;
using System;

public partial class Base : Area2D
{
    [Export] private HealthComponent health;

    public override void _Ready()
    {
        health.Died += OnDied;
    }

    private void OnDied()
    {

    }
}
