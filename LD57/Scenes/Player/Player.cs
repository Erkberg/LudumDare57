using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    [Export] private float muzzleOffset = 25f;
    [Export] private Node2D muzzle;
    [Export] private Timer shootCooldownTimer;
    [Export] private PackedScene projectileScene;
    [Export] private HealthComponent health;

    private GameInput input;

    public override void _Ready()
    {
        input = Game.inst.input;

        shootCooldownTimer.Timeout += OnShootCooldown;

        float maxHealth = Game.inst.data.GetUpgradeData(Stats.PlayerMaxHealth).initialValue;
        health.MaxHealth = maxHealth;
        health.CurrentHealth = maxHealth;
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
        Shoot();
    }

    private void Move(float delta)
    {
        Vector2 movement = input.GetMovement();
        Vector2 targetPos = new Vector2(Mathf.RoundToInt(movement.X) * World.inst.gridOffset, Mathf.RoundToInt(movement.Y) * World.inst.gridOffset);
        Position = Position.MoveToward(targetPos, delta * 1000);
    }

    private void Shoot()
    {
        Vector2 shootDir = input.GetShootDir();
        if (shootDir == Vector2.Zero) return;
        muzzle.Position = shootDir * muzzleOffset;
    }

    private void OnShootCooldown()
    {
        SpawnProjectile(muzzle.GlobalPosition, muzzle.Position.Normalized());
        CheckAdditionalShots();

        shootCooldownTimer.WaitTime = 1f / Game.inst.state.GetStatValue(Stats.PlayerAttackSpeed);
        shootCooldownTimer.Start();
    }

    private void SpawnProjectile(Vector2 spawnPos, Vector2 moveDir)
    {
        Projectile projectile = projectileScene.Instantiate<Projectile>();
        projectile.GlobalPosition = spawnPos;
        projectile.moveDir = moveDir;
        GetTree().Root.AddChild(projectile);
    }

    private void CheckAdditionalShots()
    {
        if (Game.inst.state.GetUpgradeLevel(Stats.PlayerAdditionalProjectile) > 0)
        {
            Array<Vector2> directions = new Array<Vector2>() { Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right };

            for (int i = 0; i < Game.inst.state.GetUpgradeLevel(Stats.PlayerAdditionalProjectile); i++)
            {
                Vector2 dir = directions.PickRandom();
                directions.Remove(dir);
                SpawnProjectile(GlobalPosition, dir);
            }
        }
    }

    public void OnHealthUpgraded()
    {
        float maxHealth = Game.inst.state.GetStatValue(Stats.PlayerMaxHealth);
        health.MaxHealth = maxHealth;
        health.CurrentHealth = maxHealth;
    }
}
