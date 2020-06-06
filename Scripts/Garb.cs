using Godot;
using System;

public class Garb : KinematicBody2D
{
    [Export] public int gravity = 1200;
    private Vector2 velocity = new Vector2();
    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {

    // }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, new Vector2(0, -1));
    }
}
