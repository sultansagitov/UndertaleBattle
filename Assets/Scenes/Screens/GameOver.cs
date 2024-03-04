using Godot;
using System;

public partial class GameOver : ColorRect
{
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
            GetTree().ChangeSceneToFile("res://Assets/Scenes/Screens/Main.tscn");
    }
}
