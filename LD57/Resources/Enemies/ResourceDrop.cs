using Godot;
using System;

public partial class ResourceDrop : GpuParticles2D
{
    [Export] private Timer collectTimer;

    public int value;

    private const string ResourceDropName = "ResourceDrop";

    public override void _Ready()
    {
        collectTimer.Timeout += Collect;

        Name = ResourceDropName;
    }

    public void SetValue(int amount)
    {
        value = amount;
        Amount = amount;
    }

    private void Collect()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "global_position", World.inst.GetCenter(), 0.33f);
        tween.TweenCallback(Callable.From(OnCenterReached));
    }

    private void OnCenterReached()
    {
        Game.inst.state.AddScraps(value);
        QueueFree();
    }
}
