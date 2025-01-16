using Godot;
using System;

public partial class SnakePc : CharacterBody2D
{
	private int lastDirection;
	private Vector2 playerPosition;
	[Export]
	public int Speed { get; set; } = 400;
	public void GetInput()
	{
		// Add these actions in Project Settings -> Input Map.
		Vector2 inputDir = Input.GetVector("move-left", "move-right", "move-up", "move-down");
		Velocity = inputDir * Speed;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lastDirection = 0;
		//playerPosition = new Vector2(8,8);
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
