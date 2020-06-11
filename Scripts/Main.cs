using Godot;
using System;

public class Main : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;

    [Signal] public delegate void GameOver();
    private PackedScene resgarb = GD.Load("res://Scenes/Garbage.tscn") as PackedScene;

    private Player player;
    private Node levelRoot;
    private CanvasLayer hud;
    private PlayerState playerState;
    private Vector2 defaultForce = new Vector2(0, 0);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelRoot = GetNodeOrNull("LevelRoot") as Node;
        hud = GetNodeOrNull("Hud") as CanvasLayer;
        hud.Connect("StartGame", this, "NewGame");
        player = levelRoot.GetNodeOrNull("Player") as Player;
        player.Connect("Detach", this, "OnGarbageDetach");
        player.Connect("OnKick", this, "OnGarbageKick");
        playerState = levelRoot.GetNodeOrNull("PlayerState") as PlayerState;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //     GD.Print(GetChildCount());
    // }

    ///<summary>Test Docstring
    private void NewGame()
    {
        player.Start(levelRoot.GetNodeOrNull<Node2D>("StartPlayer").GlobalPosition);
        levelRoot.GetNodeOrNull<Water>("Water").Start(levelRoot.GetNodeOrNull<Node2D>("StartWater").GlobalPosition);
        if (!OnStartCheck())
        {
            OnGarbageDetach(levelRoot.GetNodeOrNull<Node2D>("StartGarbage").GlobalPosition);
        }
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
        levelRoot.AddChild(garbage, true);
        playerState.attach = false;
    }

    public void OnPlayerHit(PhysicsBody2D body)
    {
        EmitSignal("GameOver");
        player.GameOver();
    }

    public bool OnStartCheck()
    {
        bool valuereturn = false;
        foreach (Node elements in levelRoot.GetChildren())
        {
            // Type t = elements.GetType();
            if (elements.GetType().Equals(typeof(RigidGarbage)))
            {
                valuereturn = true;
            }
        }
        return valuereturn;
    }
}
