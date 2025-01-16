using Godot;
using System;

public partial class SnakePc : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	public void GetInput()
	{
		// Add these actions in Project Settings -> Input Map.
		Vector2 inputDir = Input.GetVector("move-left", "move-right", "move-up", "move-down");
		Velocity = inputDir * Speed;
		GD.Print(Velocity);
		GD.Print(inputDir);
		GD.Print(GlobalPosition);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public override void _PhysicsProcess(double delta){
		GetInput();
		//var dir = GlobalPosition;
		// Don't move if too close to the mouse pointer.
		
		//Rotation = dir.Angle();
		MoveAndSlide();
		
	}
}
