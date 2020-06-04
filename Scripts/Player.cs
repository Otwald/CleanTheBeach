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

    private Vector2 GetInput()
    {
        velocity = new Vector2();
        if (Input.IsActionPressed("player_left"))
        {
            velocity.x -= 1;
        }
        if (Input.IsActionPressed("player_right"))
        {
            velocity.x += 1;
        }
        if (!Input.IsActionPressed("player_jump"))
        {
            velocity.y += 1;
        }
        return velocity;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        Position += GetInput().Normalized() * speed * delta;
    }
}
