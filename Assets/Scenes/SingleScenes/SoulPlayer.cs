using System;
using Godot;

public partial class SoulPlayer : CharacterBody2D
{
    [Export]
    public int ColInd = 1;

    public Vector2 velocity = Vector2.One;

    public float InitialSpeed = 100;
    public float Speed = 100;
    public float MAX_SPEED = 200;

    private int _tp = 0;
    public int TP
    {
        get { return _tp; }
        set
        {
            _tp = Math.Min(100, value);
            main.tp_bar.Size = new(value, main.tp_bar.Size.Y);
            main.tp_score.Text = $"{_tp}";
        }
    }
    private int _hp = 100;
    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            main.hp_bar.Size = new(value, main.hp_bar.Size.Y);
            main.hp_score.Text = $"{_hp} / {_maxhp}";
        }
    }
    private int _maxhp = 100;
    public int MaxHP
    {
        get { return _maxhp; }
        set
        {
            _maxhp = value;
            main.hp_bg.Size = new(value, main.hp_bg.Size.Y);
            main.hp_score.Text = $"{_hp} / {_maxhp}";
        }
    }

    private bool _immortally = false;
    public bool Immortally
    {
        get { return _immortally; }
        set
        {
            _immortally = value;
            (sprite.Material as ShaderMaterial).SetShaderParameter("immortally", value);
        }
    }

    public Main main;
    public Sprite2D sprite;
    public Timer timer;

    public override void _Ready()
    {
        main = GetParent<Main>();
        timer = GetNode<Timer>("timer");
        sprite = GetNode<Sprite2D>("Soul");
        sprite.Position = Position - new Vector2((int)Position.X, (int)Position.Y);
    }

    public void Attack(int damage)
    {
        if (!Immortally)
        {
            HP -= damage;
            Immortally = true;
            timer.Start();
        }
    }

    public void Attack(int damage, Attack bone)
    {
        if (!Immortally)
        {
            HP -= damage;
            Immortally = true;
            Speed = InitialSpeed;
            timer.Start();
            bone.GetParent().RemoveChild(bone);
        }
    }

    public void AttackToMax(int damage)
    {
        if (!Immortally)
        {
            MaxHP -= damage;
            Immortally = true;
            Speed = InitialSpeed;
            timer.Start();
        }
    }

    public void Heal(int health)
    {
        if (HP < MaxHP)
        {
            HP += health;
        }
    }

    public void Heal(int health, Attack bone)
    {
        if (HP < MaxHP)
        {
            HP += health;
            bone.GetParent().RemoveChild(bone);
        }
    }

    public void IncTP(int tp)
    {
        TP += tp;
    }

    public void OnTimerTimeout()
    {
        Immortally = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        TP = Math.Min(TP, 100);
        MaxHP = Math.Min(MaxHP, 100);
        HP = Math.Min(HP, MaxHP);

        var mouse = GetViewport().GetMousePosition();

        Velocity =
            new Vector2(
                Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
                Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
            ).Normalized() * Speed;

        if (Input.IsActionPressed("mousedown"))
        {
            var direction = (mouse / main.camera.Zoom) - Position;
            if (direction.Length() > 1f)
                Velocity += direction.Normalized() * Speed;
        }

        MoveAndSlide();

        if (Velocity == Vector2.Zero)
        {
            Speed = InitialSpeed;
        }
        else
        {
            Speed = MathF.Min(Speed * 1.01f, MAX_SPEED);
        }
        //switch (ColInd)
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

        (sprite.Material as ShaderMaterial).SetShaderParameter(
            "modulate",
            ColInd switch
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
            }
        );
    }
}
