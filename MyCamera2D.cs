using System;
using Godot;

public partial class MyCamera2D : Camera2D
{
    private double q = 0;

    public override void _PhysicsProcess(double delta)
    {
        var zoom = GetWindow().Size / new Vector2(640, 360);
        var num = ((int)Math.Round(Mathf.Min(zoom.X, zoom.Y)*10))/10;
		Zoom = new(num, num);

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
