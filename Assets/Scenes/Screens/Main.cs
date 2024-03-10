using System;
using Godot;

public partial class Main : Node2D
{
    private SoulPlayer player;
    public Location location;
    public ColorRect hp_bg;
    public ColorRect hp_bar;
    public Label hp_score;
    public ColorRect tp_bar;
    public Label tp_score;
    public Camera camera;
    // public Pause pause;

    public override void _Ready()
    {
        player = GetNode<SoulPlayer>("SoulPlayer");
        player.main = this;
        camera = GetNode<Camera>("camera");
        // pause = GetNode<Pause>("pause");

        hp_bg = GetNode<ColorRect>("HP_bg");
        hp_bar = GetNode<ColorRect>("HP");
        hp_score = GetNode<Label>("HP_score");
        tp_bar = GetNode<ColorRect>("TP");
        tp_score = GetNode<Label>("TP_score");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player.HP <= 0)
            GetTree().ChangeSceneToFile("res://Assets/Scenes/Screens/GameOver.tscn");

        hp_bg.Size = new(player.MaxHP, hp_bg.Size.Y);
        hp_bar.Size = new(player.HP, hp_bar.Size.Y);

        hp_score.Text = $"{player.HP} / {player.MaxHP}";
        //HP_score.Position = new((int)Math.Max(16, Math.Min(576, (player.HP + player.MaxHP) * 3.2 - 24)), HP_score.Position.Y);

        tp_bar.Size = new(player.TP, tp_bar.Size.Y);

        tp_score.Text = $"{player.TP}";
        //TP_score.Position = new((int)Math.Max(16, Math.Min(604, player.TP * 6.4 - 10)), TP_score.Position.Y);

        // if (Input.IsActionJustPressed("ui_cancel"))
        // {
        //     GD.Print("qw"); 
        //     pause.Visible = !pause.Visible;
        // }
    }
}
