using System;
using Godot;

public partial class Bone : Attack
{
    [Export]
    public int BoneLength = 1;

    private bool SoulInTP = false;

    // Nodes
    public Sprite2D start;
    public ColorRect center;
    public Sprite2D end;

    public CollisionShape2D coll1;
    public CollisionShape2D coll2;
    public CollisionShape2D coll3;

    public override void _Ready()
    {
        base._Ready();

        start = GetNode<Sprite2D>("start");
        center = GetNode<ColorRect>("center");
        end = GetNode<Sprite2D>("end");
        collisionShape = GetNode<CollisionShape2D>("Collision");

        int halfsize = BoneLength / 2;

        start.Position = new Vector2(start.Position.X, -(halfsize + 7));
        center.Position = new Vector2(center.Position.X, -halfsize);
        end.Position = new Vector2(end.Position.X, halfsize);

        center.Size = new Vector2(6, BoneLength);

        collisionShape.Shape = new RectangleShape2D() { Size = new(6, 8 + 2 * halfsize) };

        coll1.Position = halfsize * Vector2.Up;
        coll2.Position = halfsize * Vector2.Down;
        coll3.Shape = new RectangleShape2D() { Size = new Vector2(100, BoneLength) };
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void OnTP_BodyEntered(Node2D node2d)
    {
        if (node2d is SoulPlayer)
            SoulInTP = true;
    }

    public void OnTP_BodyExited(Node2D node2d)
    {
        if (node2d is SoulPlayer)
            SoulInTP = false;
    }
}
