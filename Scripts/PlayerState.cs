using Godot;
using System;

public class PlayerState : Node
{

    public bool grounded = true;
    public bool attach = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("/root/Root/LevelRoot/Player").Connect("OnGround", this, "OnGround");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void OnGround()
    {
        grounded = !grounded;
    }
}
