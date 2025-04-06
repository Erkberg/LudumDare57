using Godot;
using Godot.Collections;
using System;

public partial class UpgradeUI : Control
{
    [Export] private PackedScene upgradeCardScene;
    [Export] private Control upgradeCardContainer;

    public override void _Ready()
    {
        Close();
    }

    private void CreateUpgradeCards()
    {
        Array<UpgradeData> randomUpgrades = Game.inst.data.GetRandomUpgrades();
        foreach (UpgradeData upgrade in randomUpgrades)
        {
            UpgradeCard upgradeCard = upgradeCardScene.Instantiate<UpgradeCard>();
            upgradeCard.InitFromData(upgrade);
            upgradeCardContainer.AddChild(upgradeCard);
        }
    }

    public void Open()
    {
        Game.inst.SetPaused(true);
        CreateUpgradeCards();
        Visible = true;
    }

    public void Close()
    {
        foreach (Node child in upgradeCardContainer.GetChildren())
        {
            child.QueueFree();
        }

        Game.inst.SetPaused(false);
        Visible = false;
    }
}
