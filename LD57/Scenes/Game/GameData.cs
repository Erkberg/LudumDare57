using Godot;
using Godot.Collections;
using System;

public partial class GameData : Node
{
    [Export] public Array<EnemyData> enemies;

    public EnemyData GetRandomEnemyData()
    {
        return enemies.PickRandom();
    }
}
