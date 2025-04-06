using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    [Export] private PackedScene enemyScene;
    [Export] private Timer spawnTimer;

    public override void _Ready()
    {
        spawnTimer.Timeout += SpawnEnemy;

        CallDeferred(MethodName.SpawnEnemy);
    }

    private void SpawnEnemy()
    {
        Enemy enemy = enemyScene.Instantiate<Enemy>();
        GetTree().Root.AddChild(enemy);
        SetSpawnPositionAndMoveDir(enemy);
    }

    private void SetSpawnPositionAndMoveDir(Enemy enemy)
    {
        Vector2 spawnPos = Vector2.Zero;
        int offset = GD.RandRange(-1, 1);

        if (GD.Randf() < 0.5f)
        {
            spawnPos.X = GD.Randf() < 0.5f ? -1 : 1;
            spawnPos.X *= World.inst.bounds.X;
            spawnPos.Y = World.inst.bounds.Y / 2f + offset * World.inst.gridOffset;
            enemy.moveDir = spawnPos.X < 0 ? Vector2.Right : Vector2.Left;
        }
        else
        {
            spawnPos.Y = GD.Randf() < 0.5f ? -1 : 1;
            spawnPos.Y *= World.inst.bounds.Y;
            spawnPos.X = World.inst.bounds.X / 2f + offset * World.inst.gridOffset;
            enemy.moveDir = spawnPos.Y < 0 ? Vector2.Down : Vector2.Up;
        }

        enemy.GlobalPosition = spawnPos;
    }
}
