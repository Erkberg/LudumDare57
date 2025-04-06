using Godot;
using System;
using System.Collections.Generic;

public partial class GameInput : Node
{
    private Vector2 shootDir;

    private readonly Dictionary<string, Vector2> shootDirs = new()
    {
        { Inputs.ShootLeft, Vector2.Left },
        { Inputs.ShootRight, Vector2.Right },
        { Inputs.ShootUp, Vector2.Up },
        { Inputs.ShootDown, Vector2.Down }
    };

    private readonly List<string> shootDirHistory = new();

    public override void _Process(double delta)
    {
        HandleShootInput();
    }

    public Vector2 GetMovement()
    {
        return Input.GetVector(Inputs.MoveLeft, Inputs.MoveRight, Inputs.MoveUp, Inputs.MoveDown);
    }

    private void HandleShootInput()
    {
        foreach (var direction in shootDirs.Keys)
        {
            if (Input.IsActionJustReleased(direction))
            {
                shootDirHistory.Remove(direction);
            }

            if (Input.IsActionJustPressed(direction))
            {
                shootDirHistory.Add(direction);
            }
        }

        // Set motion based on the last pressed input
        if (shootDirHistory.Count > 0)
        {
            var lastDirection = shootDirHistory[^1]; // Get the last item in the list
            shootDir = shootDirs[lastDirection];
        }
    }

    public Vector2 GetShootDir()
    {
        return shootDir;
    }
}
