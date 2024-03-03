using Godot;
using System;

public partial class MyCamera2D : Camera2D
{
	private double q = 0;

	public override void _PhysicsProcess(double delta)
	{
		Zoom = new Vector2(1280, 720) / GetWindow().Size;

		if (Input.IsActionJustPressed("fullscreen"))
		{
			if (DisplayServer.WindowGetMode() != DisplayServer.WindowMode.Fullscreen)
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
			else
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}

		var quitting = GetNode<Control>("quitting");
        var m = quitting.Modulate;
        quitting.Modulate = new Color(m.R, m.G, m.B, (float)q);

		if (Input.IsActionPressed("ui_cancel"))
		{
			q += delta;

			if (q > 1.2)
			{
				GetTree().Quit();
			}
		}
		else
		{
			q = Math.Max(0, q - delta * 5);
		}
	}
}
