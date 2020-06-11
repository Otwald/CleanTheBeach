using Godot;
using System;

public class RigidGarbage : RigidBody2D
{

    [Signal] public delegate void Attach();

    private PlayerState ps;
    private Area2D groundCheck;
    private bool grounded = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ps = GetNode("/root/Root/LevelRoot/PlayerState") as PlayerState;
        GetNode("GAttach").Connect("body_entered", this, "AreaEnteredPlayer");
        groundCheck = GetNodeOrNull("GroundCheck") as Area2D;
        groundCheck.Connect("body_entered", this, "GroundEnter");
        groundCheck.Connect("body_exited", this, "GroundLeave");
        Connect("tree_exiting", this, "OnGargageAtach");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    // }

    public void GroundEnter(PhysicsBody2D body)
    {
        grounded = true;
    }

    public void GroundLeave(PhysicsBody2D body)
    {
        grounded = false;
    }
    public void AreaEnteredPlayer(RigidBody body)
    {
        if (ps.grounded)
        {
            if (grounded)
            {
                EmitSignal("Attach");
                QueueFree();
            }
        }
    }

    public void OnDetach(Vector2 pos)
    {
        Position = pos;
    }
    private void OnGargageAtach()
    {
        ps.attach = true;
    }
}
