using System;
using Godot;

public partial class Attack : Area2D
{
    [Export]
    public int ColInd = 0;

    [Export]
    public int Damage = 5;

    public Main main;

    public bool SoulIn = false;
    public SoulPlayer player;
    public CollisionShape2D collisionShape;

    public Random rnd;

    public override void _Ready()
    {
        player = GetNode<SoulPlayer>("/root/Main/SoulPlayer");

        rnd = new();

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
            _ => G.ERROR,
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if (SoulIn)
        {
            switch (ColInd)
            {
                case 0:
                    player.Attack(Damage);
                    break;
                case 1:
                    player.AttackToMax(Damage);
                    break;
                case 2:
                    if (player.Velocity == Vector2.Zero)
                        player.Attack(Damage);
                    break;
                case 3:
                    player.Attack(Damage, this);
                    break;
                case 4:
                    player.Heal(Damage, this);
                    break;
                case 5:
                    if (player.Velocity != Vector2.Zero)
                        player.Attack(Damage);
                    break;
                case 6:
                    player.IncTP(Damage);
                    break;
                case 7:
                    int num = rnd.Next(3);
                    switch (num)
                    {
                        case 0:
                            player.Attack(Damage);
                            break;
                        case 1:
                            player.AttackToMax(Damage);
                            break;
                        case 2:
                            player.Heal(Damage);
                            break;
                    }
                    break;
            }
        }
    }

    public void _on_body_entered(Node2D node2d)
    {
        if (node2d is SoulPlayer)
            SoulIn = true;
    }

    private void _on_body_exited(Node2D node2d)
    {
        if (node2d is SoulPlayer)
            SoulIn = false;
    }
}
