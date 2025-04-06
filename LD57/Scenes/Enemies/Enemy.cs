using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export] private HealthComponent health;
    [Export] private Shake2D spriteShake;
    [Export] private Sprite2D sprite;
    [Export] private CollisionShape2D collisionShape;
    [Export] private PackedScene resourceDropScene;

    public Vector2 moveDir = Vector2.Right;

    private EnemyData data;
    private float rotateSpeed;

    private const string EnemyName = "Enemy";

    public void InitFromData(EnemyData data)
    {
        this.data = data;

        health.MaxHealth = data.health;
        health.CurrentHealth = data.health;

        sprite.Texture = data.sprite;
        sprite.Scale = Vector2.One * data.spriteScale;

        RectangleShape2D rectShape = collisionShape.Shape as RectangleShape2D;
        rectShape.Size = data.colliderSize;

        Name = EnemyName + data.type;
    }

    override public void _Ready()
    {
        health.Died += OnDied;

        rotateSpeed = (float)GD.RandRange(-1f, 1f);
    }

    public override void _Process(double delta)
    {
        Move((float)delta);
        RotateSprite((float)delta);
        CheckDestroy();
    }

    private void Move(float delta)
    {
        GlobalPosition += moveDir * delta * data.speed;
    }

    private void RotateSprite(float delta)
    {
        spriteShake.Rotate(rotateSpeed * delta);
    }

    public void DoDamage(float amount)
    {
        spriteShake.StartShake();
        health.Damage(amount);
    }

    public void ApplyKnockback(float strength)
    {
        GlobalPosition -= moveDir * strength;
    }

    private void OnDied()
    {
        ResourceDrop drop = resourceDropScene.Instantiate<ResourceDrop>();
        GetTree().Root.AddChild(drop);
        drop.GlobalPosition = GlobalPosition;
        drop.SetValue((int)data.health);

        QueueFree();
    }

    private void CheckDestroy()
    {
        if (World.inst.IsOusideBounds(GlobalPosition))
        {
            //GD.Print($"Out of bounds enemy at {GlobalPosition} with dir {moveDir}");
            QueueFree();
        }
    }
}
