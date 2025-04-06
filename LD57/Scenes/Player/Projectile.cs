using Godot;
using System;

public partial class Projectile : Area2D
{
    public Vector2 moveDir;

    private int piercesUsed;

    private const string ProjectileName = "Projectile";

    override public void _Ready()
    {
        AreaEntered += OnAreaEntered;
        Name = ProjectileName;
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
        CheckDestroy();
    }

    private void Move(float delta)
    {
        GlobalPosition += moveDir * delta * Game.inst.state.GetStatValue(Stats.PlayerProjectileSpeed);
    }

    private void OnAreaEntered(Area2D other)
    {
        if (other is Enemy enemy)
        {
            enemy.DoDamage(Game.inst.state.GetStatValue(Stats.PlayerProjectileDamage));

            if (Game.inst.state.GetStatValue(Stats.PlayerProjectileKnockback) > 0)
            {
                enemy.ApplyKnockback(Game.inst.state.GetStatValue(Stats.PlayerProjectileKnockback));
            }

            if (Game.inst.state.GetStatValue(Stats.PlayerProjectilePierce) > piercesUsed)
            {
                piercesUsed++;
            }
            else
            {
                QueueFree();
            }
        }
    }

    private void CheckDestroy()
    {
        if (World.inst.IsOusideBounds(GlobalPosition))
        {
            QueueFree();
        }
    }
}
