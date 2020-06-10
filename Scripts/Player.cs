using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int speed = 500;
    [Export] public int gravity = 1200;
    [Export] public int jumpspeed = -400;
    [Export] public int kickForceX = 200;
    [Export] public int kickForceY = -200;
    [Signal] public delegate void Detach();
    [Signal] public delegate void OnGround();
    [Signal] public delegate void OnKick();


    private bool jumping = false;
    private bool doublejumping = true;
    private bool attachGarbage = false;
    private bool tempState = false;

    private bool spriteFlip = true;
    private AnimatedSprite animatedSprite;
    private Node2D leftGarbSpawn;

    private Node2D rightGarbSpawn;
    private PlayerState playerState;
    public Vector2 velocity = new Vector2();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNodeOrNull<AnimatedSprite>("AnimatedSprite");
        playerState = GetNodeOrNull("/root/Root/PlayerState") as PlayerState;
        leftGarbSpawn = GetNodeOrNull<Node2D>("LeftGarbSpawn");
        rightGarbSpawn = GetNodeOrNull<Node2D>("RightGarbSpawn");
        // Hide();
    }


    private void GetInput()
    {
        velocity.x = 0;
        bool right = Input.IsActionPressed("player_right");
        bool left = Input.IsActionPressed("player_left");
        bool jump = Input.IsActionJustPressed("player_jump");
        bool action = Input.IsActionJustPressed("player_action");
        if (right)
        {
            velocity.x += speed;
        }
        if (left)
        {
            velocity.x -= speed;
        }
        if (action && attachGarbage)
        {
            OnKickAction();
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
        if (velocity.x != 0)
        {
            spriteFlip = velocity.x < 0;
            animatedSprite.FlipH = spriteFlip;
        }
        if (jumping && IsOnFloor())
        {
            jumping = false;
            doublejumping = true;
            EmitSignal("OnGround");
        }
    }

    public void Start(Vector2 startpos)
    {
        Position = startpos;
        Show();
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

    public void OnKickAction()
    {
        attachGarbage = false;
        Vector2 kick = rightGarbSpawn.GlobalPosition;
        int tempForce = kickForceX;
        if (spriteFlip)
        {
            kick = leftGarbSpawn.GlobalPosition;
            tempForce *= -1;
        }
        EmitSignal("OnKick", kick, new Vector2(tempForce, kickForceY));
    }
}
