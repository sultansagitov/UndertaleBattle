using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public readonly float NormalSpeed = 100f;
    public readonly float BoostSpeed = 200f;

    public Focus focus;

    public AnimatedSprite2D sprite;
    public Main main;
    //public Label label;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("Sprite2D");
        main = GetNode<Main>("/root/Main");
        //label = GetNode<Label>("Label");
    }

    private void Walk(Vector2 Facing)
    {
        if (Input.IsActionPressed("boost"))
            Velocity = Facing * BoostSpeed;
        else
            Velocity = Facing * NormalSpeed;

        MoveAndSlide();

        if (Facing == Vector2.Zero)
        {
            if (sprite.Frame == 0 || sprite.Frame == 2)
                sprite.Stop();
        }
        else
        {
            sprite.Play();
        }

        if (Facing.X != 0)
        {
            if (Facing.X > 0) sprite.Animation = "right";
            if (Facing.X < 0) sprite.Animation = "left";
        }
        else
        {
            if (Facing.Y > 0) sprite.Animation = "down";
            if (Facing.Y < 0) sprite.Animation = "up";
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (focus == Focus.Pause)
            Walk(Vector2.Zero);
        else
            Walk(new Vector2(
                Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
                Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
            ));

        //label.Text = sprite.Frame + "\n" + sprite.Animation;
    }
}
