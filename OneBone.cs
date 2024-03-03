using System;
using Godot;

public partial class OneBone : Area2D
{
    [Export]
    public int ColInd = 0;

    [Export]
    public int BoneLength = 1;

    public Main main;

    private bool SoulIn = false;
    private SoulPlayer player;
    
    public override void _Ready()
	{
        player = GetNode<SoulPlayer>("/root/Main/SoulPlayer");
        var start = GetNode<Sprite2D>("start");
        var center = GetNode<ColorRect>("center");
        var end = GetNode<Sprite2D>("end");
        var collisionShape = GetNode<CollisionShape2D>("Collision");

        int halfsize = BoneLength / 2;

        start.Position = new Vector2(start.Position.X, -(halfsize + 7));
        center.Position = new Vector2(center.Position.X, -halfsize);
        end.Position = new Vector2(end.Position.X, halfsize);

        center.Size = new Vector2(6, BoneLength);

        collisionShape.Shape = new RectangleShape2D()
        {
            Size = new Vector2(2, 6 + halfsize)
        };
        collisionShape.Position = new Vector2(collisionShape.Position.X, 7 + halfsize);

        Modulate = ColInd switch
        {
            0 => G.WHITE,
            1 => G.RED,
            2 => G.ORANGE,
            3 => G.YELLOW,
            4 => G.GREEN,
            5 => G.LIGHTBLUE,
            6 => G.BLUE,
            7 => G.PURPLE,
            _ => G.WHITE,
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if ((player.GlobalPosition - GlobalPosition).Length() <= 50)
        {
            player.TP += 5 * (int)delta;
        }

        if (SoulIn)
        {
            if (!player.Immortally)
            {
                switch (ColInd)
                {
                    case 0:
                        player.Attack(5);
                        break;
                    case 1:
                        player.AttackRed(5);
                        break;
                    case 2:
                        if (player.Velocity == Vector2.Zero)
                        {
                            player.Attack(5);
                        }
                        break;
                    case 4:
                        player.HP += 5;
                        GetParent().RemoveChild(this);
                        break;
                    case 5:
                        if (player.Velocity != Vector2.Zero)
                        {
                            player.Attack(5);
                        }
                        break;
                }
            }
        }
    }

    private void _on_OneBone_body_entered(Node body)
    {
        if (body is Player)
            SoulIn = true;
    }

    private void _on_OneBone_body_exited(Node body)
    {
        if (body is Player)
            SoulIn = false;
    }
}
