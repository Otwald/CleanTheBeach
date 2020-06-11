using Godot;
using System;

public class GarbCan : StaticBody2D
{
    [Signal] public delegate void OnLevelEnd();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNodeOrNull("WinPlane").Connect("body_entered", this, "OnPlayerContact");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void OnPlayerContact(PhysicsBody2D body)
    {
        EmitSignal("OnLevelEnd");
    }
}
