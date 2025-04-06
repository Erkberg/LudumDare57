using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameData : Node
{
    [Export] public Array<EnemyData> enemies;
    [Export] public Array<UpgradeData> upgrades;

    public EnemyData GetRandomEnemyData()
    {
        return enemies.PickRandom();
    }

    public UpgradeData GetUpgradeData(string statName)
    {
        return (UpgradeData)upgrades.Where(x => x.name == statName);
    }

    public bool IsMaxLevel(string statName)
    {
        UpgradeData upgrade = GetUpgradeData(statName);
        return upgrade.maxLevel <= Game.inst.state.GetUpgradeLevel(statName);
    }

    public Array<UpgradeData> GetRandomUpgrades()
    {
        Array<UpgradeData> filteredUpgrades = (Array<UpgradeData>)upgrades.Where(x => !IsMaxLevel(x.name));
        Array<UpgradeData> randomUpgrades = new Array<UpgradeData>();
        int numUpgrades = 3;
        for (int i = 0; i < numUpgrades; i++)
        {
            UpgradeData upgrade = GetUpgradeData(filteredUpgrades.PickRandom().name);
            randomUpgrades.Add(upgrade);
            filteredUpgrades.Remove(upgrade);
        }
        return randomUpgrades;
    }
}