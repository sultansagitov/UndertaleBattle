using System;
using Godot;

public partial class Bone : Attack
{
    [Export]
    public int BoneLength = 1;

    public override void _Ready()
    {
        base._Ready();

        var start = GetNode<Sprite2D>("start");
        var center = GetNode<ColorRect>("center");
        var end = GetNode<Sprite2D>("end");
        collisionShape = GetNode<CollisionShape2D>("Collision");

        int halfsize = BoneLength / 2;

        start.Position = new Vector2(start.Position.X, -(halfsize + 7));
        center.Position = new Vector2(center.Position.X, -halfsize);
        end.Position = new Vector2(end.Position.X, halfsize);

        center.Size = new Vector2(6, BoneLength);

        collisionShape.Shape = new RectangleShape2D() { Size = new(6, 8 + 2 * halfsize) };
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
