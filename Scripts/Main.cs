using Godot;
using System;

public class Main : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene resgarb = GD.Load("res://Scenes/Garbage.tscn") as PackedScene;

    private Player player;
    private PlayerState playerState;
    private Vector2 defaultForce = new Vector2(0, 0);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetNodeOrNull("Player") as Player;
        player.Connect("Detach", this, "OnGarbageDetach");
        player.Connect("OnKick", this, "OnGarbageKick");
        playerState = GetNodeOrNull("PlayerState") as PlayerState;
        NewGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //     GD.Print(GetChildCount());
    // }

    private void NewGame()
    {
        // player.Start(GetNodeOrNull<Area2D>("StartPosition").GlobalPosition);
    }

    private void OnGarbageDetach(Vector2 pos)
    {
        SpawnGarbage(pos, new Vector2(0, 0));
    }

    private void OnGarbageKick(Vector2 pos, Vector2 force)
    {
        SpawnGarbage(pos, force);
    }

    private void SpawnGarbage(Vector2 pos, Vector2 force)
    {
        var garbage = (RigidGarbage)resgarb.Instance();
        garbage.OnDetach(pos);
        garbage.ApplyCentralImpulse((Vector2)force);
        AddChild(garbage, true);
        playerState.attach = false;
    }

    public void OnPlayerHit(PhysicsBody2D body)
    {
        GD.Print("Hit");
        player.QueueFree();
    }
}
