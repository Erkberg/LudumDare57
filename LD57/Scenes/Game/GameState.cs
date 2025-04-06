using Godot;
using System;

public partial class GameState : Node
{
    [Export] public int Scraps { get; set; }
    [Export] public int Level { get; set; }

    public Action onLevelUp;

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
}
