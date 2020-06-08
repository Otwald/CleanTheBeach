using Godot;
using System;

public class Garb : KinematicBody2D
{
    [Export] public int gravity = 1200;
    [Export] public int speed = 500;
    [Signal] public delegate void Detach();
    private Vector2 velocity = new Vector2();

    private RayCast2D leftR;
    private RayCast2D rightR;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("/root/Root/Garbage").Connect("Attach", this, "GarbageAttached");
        leftR = GetNodeOrNull<RayCast2D>("Left");
        rightR = GetNodeOrNull<RayCast2D>("Right");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity.y += gravity * delta;
        if (leftR.IsColliding())
        {
            PushGarb(leftR, "left");
        }
        if (rightR.IsColliding())
        {
            PushGarb(rightR, "right");
        }
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));

    }

    private void PushGarb(RayCast2D ray, string direct)
    {
        Vector2 contact = ray.GetCollisionPoint();
        var distance = Position.DistanceTo(contact);
        if (distance < 8.0f)
        {
            velocity.x = 0;
            bool right = Input.IsActionPressed("player_right");
            bool left = Input.IsActionPressed("player_left");
            if (right && (direct == "left"))
            {
                velocity.x += speed;
            }
            if (left && (direct == "right"))
            {
                velocity.x -= speed;
            }
        }
        else
        {
            EmitSignal("Detach");
        }
    }

    public void GarbageAttached(Vector2 pos)
    {
        Show();
        Position = pos;
    }
}
