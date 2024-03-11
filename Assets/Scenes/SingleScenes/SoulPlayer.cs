using System;
using Godot;

public partial class SoulPlayer : CharacterBody2D
{
    [Export]
    public int ColInd = 1;

    public Vector2 velocity = Vector2.One;

    public float Speed = 100;

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
            _hp = Math.Min(MaxHP, value);
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
            _maxhp = Math.Min(100, value);
            HP = Math.Min(HP, MaxHP);
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

    // Nodes
    public Main main;
    public Sprite2D sprite;
    public Sprite2D sprite_tpcircle;
    public Timer imm_timer;
    public Timer tp_timer;

    public void Attack(int damage)
    {
        if (!Immortally)
        {
            HP -= damage;
            Immortally = true;
            imm_timer.Start();
        }
    }

    public void Attack(int damage, Attack bone)
    {
        if (!Immortally)
        {
            HP -= damage;
            Immortally = true;
            imm_timer.Start();
            bone.GetParent().RemoveChild(bone);
        }
    }


    public void AttackToMax(int damage)
    {
        if (!Immortally)
        {
            MaxHP -= damage;
            Immortally = true;
            imm_timer.Start();
        }
    }

    public void OnImmtimerTimeout()
    {
        Immortally = false;
    }

    public void Heal(int health)
    {
        HP += health;
    }

    public void Heal(int health, Attack bone)
    {
        if (HP < MaxHP)
            bone.GetParent().RemoveChild(bone);

        HP += health;
    }

    public void IncTP(int tp)
    {
        if ((TP += tp) < 100)
        {
            sprite_tpcircle.Visible = true;
            tp_timer.Start();
        }
    }

    public void OnTPtimerTimeout()
    {
        sprite_tpcircle.Visible = false;
    }

    public override void _Ready()
    {
        main = GetParent<Main>();
        imm_timer = GetNode<Timer>("imm-timer");
        tp_timer = GetNode<Timer>("tp-timer");
        sprite = GetNode<Sprite2D>("Soul");
        sprite_tpcircle = GetNode<Sprite2D>("CircleTp");
        sprite.Position = Position - new Vector2((int)Position.X, (int)Position.Y);
    }

    public override void _PhysicsProcess(double delta)
    {
        var mouse = GetViewport().GetMousePosition();

        if (Input.IsActionPressed("mousedown"))
        {
            var direction = (mouse / main.camera.Zoom) - Position;
            // if (direction.Length() > 4f)
            Velocity = direction.Normalized() * Speed;
        }
        else
        {
            Velocity =
                new Vector2(
                    Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
                    Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
                ).Normalized() * Speed;
        }

        MoveAndSlide();

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
