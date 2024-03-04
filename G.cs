using System;
using Godot;

public partial class G : Node
{
    public static readonly Color WHITE = new(1, 1, 1, 1);
    public static readonly Color RED = new(1, 0, 0, 1);
	public static readonly Color ORANGE = new(1, 0.5f, 0, 1);
	public static readonly Color YELLOW = new(1, 1, 0, 1);
	public static readonly Color GREEN = new(0, 1, 0, 1);
	public static readonly Color LIGHTBLUE = new(0, 0.5f, 1, 1);
    public static readonly Color BLUE = new(0, 0, 1, 1);
    public static readonly Color PURPLE = new(0.5f, 0, 1, 1);

	public static readonly Color ERROR = new(1, 0, 1, 1);

    public static Vector2 Trigvec(float angle) => new(Mathf.Cos(angle), Mathf.Sin(angle));
    public static Vector2 Intvec(float vec) => new((int)vec, (int)vec);
}
