using Godot;
using System;

public partial class UpgradeCard : Control
{
    [Export] private Label nameLabel;
    [Export] private Button pickButton;

    private UpgradeData data;

    public override void _Ready()
    {
        pickButton.Pressed += OnPicked;
    }

    public void InitFromData(UpgradeData data)
    {
        this.data = data;

        nameLabel.Text = data.name;
    }

    private void OnPicked()
    {
        Game.inst.state.LevelUpStat(data.stat);
        World.inst.ui.upgradeUI.Close();
    }
}
