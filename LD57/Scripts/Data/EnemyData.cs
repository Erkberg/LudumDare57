using Godot;
using System;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export] public string type;
    [Export] public Texture2D sprite;
    [Export] public float health = 1f;
    [Export] public float speed = 100f;
    [Export] public float damage = 1f;
    [Export] public float spriteScale = 0.2f;
    [Export] public Vector2 colliderSize = new Vector2(50f, 50f);
}
