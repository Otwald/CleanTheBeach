using Godot;
using System;

public class Hud : CanvasLayer
{
    [Signal] public delegate void StartGame();

    private Button start;
    private Label msg;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        msg = GetNodeOrNull("Message") as Label;
        start = GetNodeOrNull("Start") as Button;
        start.Connect("pressed", this, "OnPressStart");
        GetParent().Connect("GameOver", this, "OnReset");
        OnStart();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void OnStart()
    {
        msg.Text = "Clean the Beach!";
        start.Text = "Start!";
    }

    private void OnReset()
    {
        msg.Text = "More Cleaning!";
        msg.Show();
        start.Text = "Restart!";
        start.Show();

    }
    public void OnPressStart()
    {
        start.Hide();
        msg.Hide();
        EmitSignal("StartGame");
    }

}
