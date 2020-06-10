using Godot;
using System;

public class Water : KinematicBody2D
{
    // Called when the node enters the scene tree for the first time.
    [Export] public int waterSpeed = 200;
    private Vector2 velocity = new Vector2(1, 0);
    public override void _Ready()
    {
        GetNodeOrNull("KillPlane").Connect("body_entered", GetParent(), "OnPlayerHit");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity = MoveAndSlide(velocity);
    }

    public void Start()
    {
        velocity.x = waterSpeed;
    }
}
