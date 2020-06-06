using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int speed = 500;
    [Export] public int gravity = 1200;
    [Export] public int jumpspeed = -400;

    private bool jumping = false;
    private bool doublejumping = true;
    private Vector2 velocity = new Vector2();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    private void GetInput()
    {
        velocity.x = 0;
        bool right = Input.IsActionPressed("player_right");
        bool left = Input.IsActionPressed("player_left");
        bool jump = Input.IsActionJustPressed("player_jump");
        if (right)
        {
            velocity.x += speed;
        }
        if (left)
        {
            velocity.x -= speed;
        }
        if (jump && IsOnFloor())
        {
            jumping = true;
            velocity.y = jumpspeed;
        }
        if (doublejumping && jump && !IsOnFloor())
        {
            doublejumping = false;
            velocity.y = jumpspeed;
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        GetInput();
        velocity.y += gravity * delta;
        if (jumping && IsOnFloor())
        {
            jumping = false;
            doublejumping = true;
        }
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
    }
}
