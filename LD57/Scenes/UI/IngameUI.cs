using Godot;
using System;

public partial class IngameUI : CanvasLayer
{
    [Export] public ScrapsUI scrapsUI;
    [Export] public UpgradeUI upgradeUI;

    public override void _Ready()
    {
        Game.inst.state.onLevelUp += OnLevelUp;
    }

    private void OnLevelUp()
    {
        upgradeUI.Open();
    }
}
