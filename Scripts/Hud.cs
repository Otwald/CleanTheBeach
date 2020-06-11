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
        GetParent().Connect("Win", this, "OnWin");
        OnStart();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    private void handleMessage(string msgT, string buttonT)
    {
        msg.Text = msgT;
        msg.Show();
        start.Text = buttonT;
        start.Show();
    }
    private void OnStart()
    {
        handleMessage("Clean the Beach!", "Start!");
    }

    private void OnReset()
    {
        handleMessage("More Cleaning!", "Restart!");
    }
    public void OnPressStart()
    {
        start.Hide();
        msg.Hide();
        EmitSignal("StartGame");
    }

    public void OnWin()
    {
        handleMessage("The Beach is now Clean!", "Restart!");
    }

}
