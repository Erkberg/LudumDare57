using Godot;
using System;

public partial class GameInput : Node
{
    public float GetMovement()
    {
        return Input.GetActionStrength(Inputs.MoveRight) - Input.GetActionStrength(Inputs.MoveLeft);
    }

    public bool GetJumpDown()
    {
        return Input.IsActionJustPressed(Inputs.Jump);
    }

    public bool GetDashDown()
    {
        return Input.IsActionJustPressed(Inputs.Dash);
    }
}
