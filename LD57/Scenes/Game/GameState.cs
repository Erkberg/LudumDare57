using Godot;
using System;
using System.Collections.Generic;

public partial class GameState : Node
{
    [Export] public int Scraps { get; set; }
    [Export] public int Level { get; set; }

    private Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    public Action onLevelUp;

    public override void _Ready()
    {
        foreach (UpgradeData upgrade in Game.inst.data.upgrades)
        {
            upgradeLevels.Add(upgrade.name, 0);
        }
    }

    public void AddScraps(int amount)
    {
        Scraps += amount;
        if (Scraps >= GetScrapsForNextLevel())
        {
            Level++;
            Scraps -= GetScrapsForNextLevel();
            onLevelUp?.Invoke();
        }
    }

    public int GetScrapsForNextLevel()
    {
        return Level + 1 * 20;
    }

    public float GetScrapsForNextLevelPercentage()
    {
        return (float)Scraps / GetScrapsForNextLevel();
    }

    public void LevelUpStat(string statName)
    {
        upgradeLevels[statName]++;

        if (statName == Stats.PlayerMaxHealth)
        {
            World.inst.player.OnHealthUpgraded();
        }
    }

    public int GetUpgradeLevel(string statName)
    {
        return upgradeLevels[statName];
    }

    public float GetStatValue(string statName)
    {
        UpgradeData upgrade = Game.inst.data.GetUpgradeData(statName);
        return upgrade.initialValue + upgradeLevels[statName] * upgrade.valuePerLevel;
    }
}
