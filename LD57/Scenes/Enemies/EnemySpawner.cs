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
        EnemyData enemyData = Game.inst.data.GetRandomEnemyData();
        enemy.InitFromData(enemyData);
    }

    private void SetSpawnPositionAndMoveDir(Enemy enemy)
    {
        Vector2 spawnPos = Vector2.Zero;
        int offset = GD.RandRange(-1, 1);

        if (GD.Randf() < 0.5f)
        {
            float spawnDirX = GD.Randf() < 0.5f ? -1 : 1;
            spawnPos.X = World.inst.GetCenter().X + spawnDirX * (World.inst.bounds.X / 2f + World.inst.spawnOffset.X);
            spawnPos.Y = World.inst.bounds.Y / 2f + offset * World.inst.gridOffset;
            enemy.moveDir = spawnPos.X < 0 ? Vector2.Right : Vector2.Left;
        }
        else
        {
            float spawnDirY = GD.Randf() < 0.5f ? -1 : 1;
            spawnPos.Y = World.inst.GetCenter().Y + spawnDirY * (World.inst.bounds.Y / 2f + World.inst.spawnOffset.Y);
            spawnPos.X = World.inst.bounds.X / 2f + offset * World.inst.gridOffset;
            enemy.moveDir = spawnPos.Y < 0 ? Vector2.Down : Vector2.Up;
        }

        enemy.GlobalPosition = spawnPos;
    }
}
