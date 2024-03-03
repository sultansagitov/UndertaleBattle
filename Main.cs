using System;
using Godot;

public partial class Main : Node2D
{
    private SoulPlayer player;
    public Location location;

    public override void _Ready()
    {
        player = GetNode<SoulPlayer>("SoulPlayer");
        player.main = this;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player.HP <= 0)
            GetTree().ChangeSceneToFile("res://GameOver.tscn");

        var HP_bg = GetNode<ColorRect>("HP_bg");
        var HP_bar = GetNode<ColorRect>("HP");
        var HP_score = GetNode<Label>("HP_score");
        var TP_bar = GetNode<ColorRect>("TP");
        var TP_score = GetNode<Label>("TP_score");

        HP_bg.Size = new(player.MaxHP, HP_bg.Size.Y);
        HP_bar.Size = new(player.HP, HP_bar.Size.Y);

        HP_score.Text = $"{player.HP} / {player.MaxHP}";
        //HP_score.Position = new((int)Math.Max(16, Math.Min(576, (player.HP + player.MaxHP) * 3.2 - 24)), HP_score.Position.Y);

        TP_bar.Size = new(player.TP, TP_bar.Size.Y);

        TP_score.Text = $"{player.TP}";
        //TP_score.Position = new((int)Math.Max(16, Math.Min(604, player.TP * 6.4 - 10)), TP_score.Position.Y);
    }
}
