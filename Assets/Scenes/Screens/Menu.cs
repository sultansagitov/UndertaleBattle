using Godot;
using System;

public partial class Menu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{		
        if (Input.IsActionJustPressed("ui_accept"))
            GetTree().ChangeSceneToFile("res://Assets/Scenes/Screens/Main.tscn");
	}

	public void _OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://Assets/Scenes/Screens/Main.tscn");
	}
}
