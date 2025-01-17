using Godot;
using System;

public partial class SnakePc : CharacterBody2D
{
	private int lastDirection; //0 - not moving, 1 - up, 2 - right, 3 - down, 4 - left
	private Vector2 playerPosition;
	[Export]
	public int Speed { get; set; } = 400;
	[Export] private float delaySeconds = 1f;
	[Export] private float moveTimer = 0f;
	[Export] private RayCast2D _rayUp;
	[Export] private RayCast2D _rayRight;
	[Export] private RayCast2D _rayDown;
	[Export] private RayCast2D _rayLeft;

	
	public Vector2 GetInput()
	{
		// Add these actions in Project Settings -> Input Map.
		Vector2 inputDir = Input.GetVector("move-left", "move-right", "move-up", "move-down");
		Velocity = inputDir * Speed;
		//GD.Print(GlobalPosition);
		return inputDir;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lastDirection = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Set last direction here:
		Vector2 dir = GetInput();
		if(dir.X != 0)
		{
			if(dir.X > 0)
			{
				lastDirection = 2;
			}
			else
			{
				lastDirection = 4;
			}
		}
		else if(dir.Y != 0)
		{
			if(dir.Y > 0)
			{
				lastDirection = 1;
			}
			else
			{
				lastDirection = 3;
			}
		}
	}
	public override void _PhysicsProcess(double delta){
		if(moveTimer < delaySeconds)
		{
			moveTimer += (float)delta;
			return;
		}
		else
		{
			moveTimer = 0f;
		}
		Vector2 dir = GetInput();
		int pixels = 16;
		float playerX = GlobalPosition.X;
		float playerY = GlobalPosition.Y;
			//auto move
		switch(lastDirection){
			case 1://up
				playerY += 16;
				CheckCollision(_rayDown);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 2://right
				playerX += 16;
				CheckCollision(_rayRight);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 3://down
				playerY -= 16;
				CheckCollision(_rayUp);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 4://left
				playerX -= 16;
				CheckCollision(_rayLeft);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				
				break;
			default:
				break;
		}
	}
	private void CheckCollision(RayCast2D ray)
	{
		if (!ray.IsColliding())
		{
			return;
		}
		//collision occured
		GodotObject obj = ray.GetCollider();
		if (obj is TileMap tileMap)
		{
			//kill player
			GetTree().Paused = true;
			GD.Print("Snake hit a wall");
		}
		else
		{
			//hit a fruit or so snake body
			GD.Print("Snake hit something else");
		}
	}
}
