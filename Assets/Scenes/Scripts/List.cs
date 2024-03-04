using Godot;
using Godot.Collections;

public partial class List : Node2D
{
	[Export]
	public int Cols;
	[Export]
	public int Rows;

	[Export] 
	public Vector2 Gap;

	[Export]
	public string[] Data;

	//[Export]
	//public bool ChangeColor = true;

	//[Export]
	//public Color[] Colors;

	public int Col = 0;
	public int Row = 0;

	public int Index { get { return Row + Col * Rows; } }
	public Sprite2D soulOut;

	public override void _Ready()
	{
		Reset();
	}

	public void Reset()
	{
		foreach (Node node in GetChildren())
			if (node is Marker2D)
				RemoveChild(node);

		for (int ICol = 0; ICol < Cols; ICol++)
		{
			for (int IRow = 0; IRow < Rows; IRow++)
			{
                Marker2D pos = new()
                {
                    Name = "pos-" + ICol.ToString() + "-" + IRow.ToString(),
                    Position = new Vector2(ICol * Gap.X + 3, IRow * Gap.Y + 3)
                };
                AddChild(pos);

                Label shadow = new()
                {
                    Name = "Shadow",
                    Text = Data[IRow + ICol * Rows],
                    Position = new Vector2(13, -3),
                    Modulate = new Color("ff0000")
                };
                shadow.Set("custom_fonts/font", GD.Load("res://Assets/Resources/8bitOperatorPlus-Bold.ttf"));
				pos.AddChild(shadow);

                Label label = new ()
                {
                    Text = Data[IRow + ICol * Rows],
                    //if (!ChangeColor)
                    //    label.Modulate = Colors[IRow + ICol * Rows];
                    Position = new Vector2(12, -4)
                };
                label.Set("custom_fonts/font", GD.Load("res://Assets/Resources/8bitOperatorPlus-Bold.ttf"));
				pos.AddChild(label);
			}
		}
	}
}
