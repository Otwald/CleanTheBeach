using Godot;
using System;

public class RigidGarbage : RigidBody2D
{

    [Signal] public delegate void Attach();

    private PlayerState ps;
    private Player player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ps = GetNode("/root/Root/PlayerState") as PlayerState;
        player = GetNode("/root/Root/Player") as Player;
        GetNode("GAttach").Connect("body_entered", this, "AreaEnteredPlayer");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void AreaEnteredPlayer(RigidBody body)
    {
        if (ps.grounded)
        {
            EmitSignal("Attach", Position);
            QueueFree();
        }
    }

    public void OnDetach(Vector2 pos)
    {
        Position = pos;
    }
}
