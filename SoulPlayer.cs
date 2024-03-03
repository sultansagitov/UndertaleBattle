using Godot;
using System;

public partial class SoulPlayer : CharacterBody2D
{
    [Export]
    public int colind = 0;

    public Vector2 velocity = Vector2.One;
    public float speed = 100;
    public double t;

    public int TP = 100;
    public int HP = 100;
    public int MaxHP = 100;

    private bool _immortally = false;
    public bool Immortally
    {
        get
        {
            return _immortally;
        }
        set
        {
            _immortally = value;
            sprite.Set("shader_param/immortal", true);
        }
    }

    public Main main;
    public Sprite2D sprite;
    public Timer timer;

    public override void _Ready()
    {
        main = GetParent<Main>();
        sprite = GetNode<Sprite2D>("Soul");
        sprite.Position = Position - new Vector2((int)Position.X, (int)Position.Y);
    }

    public void Attack(double damage)
    {
        if (!Immortally)
        {
            HP -= (int)damage;
            Immortally = true;
            timer.Start();
        }
    }

    public void AttackRed(double damage)
    {
        if (!Immortally)
        {
            MaxHP -= (int)damage;
            Immortally = true;
            timer.Start();
        }
    }

    public void _OnTimerTimeout()
    {
        Immortally = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        TP = Math.Min(TP, 100);
        MaxHP = Math.Min(MaxHP, 100);
        HP = Math.Min(HP, MaxHP);
        
        Vector2 v = new (velocity.X, velocity.Y);
        velocity.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        velocity.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        //switch (colind)
        //{
        //    case 1:
        //        Move(velocity);
        //        break;
        //    case 2:
        //        if (velocity.Length() > 0.1f)
        //            Move(velocity);
        //        else
        //            Move(v);
        //        break;
        //    case 5:
        //        Move(velocity);
        //        if (velocity.Length() > 0.1f && !main.Immortally)
        //            Attack(6);
        //        break;
        //}

        sprite.Modulate = colind switch
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

        t += delta;
    }
}
