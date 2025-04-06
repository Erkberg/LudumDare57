using Godot;
using System;

public partial class ScrapsUI : Control
{
    [Export] private ProgressBar progressBar;
    [Export] private Label levelLabel;

    private const string LevelPrefix = "Level ";

    public override void _Process(double delta)
    {
        UpdateUI((float)delta);
    }

    private void UpdateUI(float delta)
    {
        progressBar.Value = Mathf.Lerp(progressBar.Value, Game.inst.state.GetScrapsForNextLevelPercentage(), delta * 3f);
        levelLabel.Text = $"{LevelPrefix} {Game.inst.state.Level}";
    }
}
