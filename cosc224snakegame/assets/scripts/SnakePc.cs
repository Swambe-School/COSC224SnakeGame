using Godot;
using System;

public partial class SnakePc : CharacterBody2D
{
	[Export]
	public int Speed = 100;
	public void GetInput()
	{
		// Add these actions in Project Settings -> Input Map.
		Vector2 inputDir = Input.GetVector("move-left", "move-right", "move-up", "move-down");
		Velocity = Transform.X * inputDir * Speed;
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
		var dir = GetGlobalMousePosition() - GlobalPosition;
		// Don't move if too close to the mouse pointer.
		if (dir.Length() > 5)
		{
			Rotation = dir.Angle();
			MoveAndSlide();
		}
	}
}
