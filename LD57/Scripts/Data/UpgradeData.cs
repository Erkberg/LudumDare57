using Godot;
using System;

[GlobalClass]
public partial class UpgradeData : Resource
{
    [Export] public string stat;
    [Export] public float initialValue;
    [Export] public float valuePerLevel;
    [Export] public string name;
    [Export] public string description;
    [Export] public int maxLevel = 3;
    [Export] public Texture icon;
}
