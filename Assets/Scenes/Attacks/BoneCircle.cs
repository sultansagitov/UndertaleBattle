using Godot;
using System;

public partial class BoneCircle : Node2D
{
	[Export]
	public int Colind = 0;

	[Export]
	public int n;

	[Export]
	public int Radius = 100;

	[Export]
	public int BoneLength = 6;

	private double Time = 0;

	public override void _Ready()
	{
		for (int i = 0; i < n; i++)
		{
			var bone = GD.Load<PackedScene>("res://Assets/Scenes/Attacks/Bone.tscn").Instantiate<Bone>();
			bone.BoneLength = BoneLength;
			bone.Name = "Bone_" + i.ToString();
			bone.ColInd = i % 8;
			bone.Position = new(
				(float)Math.Cos(2 * Math.PI * i / n) * Radius,
				(float)Math.Sin(2 * Math.PI * i / n) * Radius
			);

            AddChild(bone);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		for (int i = 0; i < n; i++)
		{
			if (HasNode("Bone_" + i.ToString()))
			{
				var p = new Vector2(
					(float)Math.Cos(2 * Math.PI * i / n + Time * 0.6) * Radius, 
					(float)Math.Sin(2 * Math.PI * i / n + Time * 0.6) * Radius
				);
				GetNode<Node2D>("Bone_" + i.ToString()).Position = p;
			}
		}

		Time += delta;
	}
}
