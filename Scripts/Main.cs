using Godot;
using System;

public class Main : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene resgarb = GD.Load("res://Scenes/Garbage.tscn") as PackedScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("Player").Connect("Detach", this, "OnGarbageDetach");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
    //     GD.Print(GetChildCount());
    // }

    private void OnGarbageDetach(Vector2 pos)
    {
        var garbage = (RigidGarbage)resgarb.Instance();
        garbage.OnDetach(pos);
        AddChild(garbage, true);
    }
}
