using Godot;
using System;

public partial class ScoreTracker : Node
{
	RichTextLabel scoreLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scoreLabel = GetNode<RichTextLabel>("Score");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		scoreLabel.Text = " Score: " + GameController.getInstance().getScore() + " "; 
	}

}
