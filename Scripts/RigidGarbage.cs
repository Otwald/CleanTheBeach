using Godot;
using System;

public class RigidGarbage : RigidBody2D
{

    [Signal] public delegate void Attach();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
        GetNode("GAttach").Connect("body_entered", this, "AreaEnteredPlayer");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void AreaEnteredPlayer(RigidBody body)
    {
        Hide();
        EmitSignal("Attach", Position);
        QueueFree();
    }
}
