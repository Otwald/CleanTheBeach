using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public int speed = 2;

    private Vector2 velocity = new Vector2();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity = new Vector2();
        var rotation = GetGlobalMousePosition().AngleToPoint(Position);
        if (Input.IsActionPressed("player_left"))
        {
            velocity.x -= 1;
        }
        if (Input.IsActionPressed("player_right"))
        {
            velocity.x += 1;
        }
        Position += velocity.Normalized() * speed * delta;
    }
}
