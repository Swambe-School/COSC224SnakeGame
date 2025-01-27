using Godot;
using System;

public partial class GameOver : Node
{
	private int finalScore;
	private Camera2D camera;
	private RichTextLabel scoreLabel;
	private Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = false;
		camera = GetNode<Camera2D>("Camera");
		scoreLabel = GetNode<RichTextLabel>("finalScore");
		scoreLabel.Text = "Your score is: " + finalScore;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_return_pressed()
	{
		Node scene = ResourceLoader.Load<PackedScene>("res://scenes/main_menu.tscn").Instantiate();
		GetTree().Root.AddChild(scene); 
		camera.Enabled = false;
		this.QueueFree();
	}
	
	public void setFinalScore(int score)
	{
		this.finalScore = score;
	}
}
