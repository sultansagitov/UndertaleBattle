using System;
using Godot;

public partial class Attack : Area2D
{
    [Export]
    public int ColInd = 0;

    public Main main;

    public bool SoulIn = false;
    public SoulPlayer player;
    public CollisionShape2D collisionShape;       

    public Random rnd;

    public override void _Ready()
    {
        player = GetNode<SoulPlayer>("/root/Main/SoulPlayer");
        collisionShape = GetNode<CollisionShape2D>("Collision");

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
            _ => G.WHITE,
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if (SoulIn)
        {
            switch (ColInd)
            {
                case 0:
                    player.Attack(5);
                    break;
                case 1:
                    player.AttackToMax(5);
                    break;
                case 2:
                    if (player.Velocity == Vector2.Zero)
                        player.Attack(5);
                    break;
                case 3:
                    player.Attack(5, this);
                    break;
                case 4:
                    player.Heal(5, this);
                    break;
                case 5:
                    if (player.Velocity != Vector2.Zero)
                        player.Attack(5);
                    break;
                case 7:
                    int num = rnd.Next(2);
                    switch (num) {
                        case 0:
                            player.Attack(5);
                            break; 
                        case 1:
                            player.AttackToMax(5);
                            break; 
                        case 2:
                            player.Heal(5);
                            break; 
                    }
                    break;
            }
        }
    }

    private void OnBoneBodyEntered(Node body)
    {
        if (body is SoulPlayer)
            SoulIn = true;
    }

    private void OnBoneBodyExited(Node body)
    {
        if (body is SoulPlayer)
            SoulIn = false;
    }
}
