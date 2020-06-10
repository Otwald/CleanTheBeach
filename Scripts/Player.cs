using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int speed = 500;
    [Export] public int gravity = 1200;
    [Export] public int jumpspeed = -400;
    [Signal] public delegate void Detach();
    [Signal] public delegate void OnGround();


    private bool jumping = false;
    private bool doublejumping = true;
    private bool attachGarbage = false;
    private bool tempState = false;
    private AnimatedSprite animatedSprite;
    private PlayerState playerState;
    public Vector2 velocity = new Vector2();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNodeOrNull<AnimatedSprite>("AnimatedSprite");
        playerState = GetNodeOrNull("/root/Root/PlayerState") as PlayerState;
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
            EmitSignal("OnGround");
            if (attachGarbage)
            {
                EmitSignal("Detach", Position);
            }
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
        CheckSprite();
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
        if (jumping && IsOnFloor())
        {
            jumping = false;
            doublejumping = true;
            EmitSignal("OnGround");
        }
    }

    public void CheckSprite()
    {
        if (tempState == playerState.attach) { return; }
        if (playerState.attach)
        {
            animatedSprite.Animation = "Attach";
            attachGarbage = true;
        }
        else
        {
            animatedSprite.Animation = "default";
            attachGarbage = false;
        }
        tempState = playerState.attach;
    }
}
