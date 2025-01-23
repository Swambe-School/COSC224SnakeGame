using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SnakePc : CharacterBody2D
{
	private LinkedList<BodyPart> _snakeBodySegments;
	private Vector2 _lastPosition;
	[Export] private Sprite2D _apple;
	[Export] private AnimatedSprite2D _headSprite;
	private int score = 0;
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
		_snakeBodySegments = new();

		Random rand = new Random();
		_apple.SetGlobalPosition(new Vector2(16 * (rand.Next(2, 17)) - 8, 16 * rand.Next(2, 17) - 8));
		lastDirection = 0;
		GD.Print("Apple");
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
		bool ate = false;
		float playerX = GlobalPosition.X;
		float playerY = GlobalPosition.Y;

		_lastPosition = new Vector2(playerX, playerY);
			//auto move
		switch(lastDirection){
			case 1://up
				playerY += 16;
				_headSprite.Rotation = 0;
				ate = CheckCollision(_rayDown);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 2://right
				playerX += 16;
				_headSprite.Rotation = Mathf.Pi * -0.5f;
				ate = CheckCollision(_rayRight);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 3://down
				playerY -= 16;
				_headSprite.Rotation = Mathf.Pi;
				ate = CheckCollision(_rayUp);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				break;
			case 4://left
				playerX -= 16;
				_headSprite.Rotation = Mathf.Pi * 0.5f;
				ate = CheckCollision(_rayLeft);
				this.SetGlobalPosition(new Vector2(playerX, playerY));
				
				break;
			default:
				break;
		}
		if(lastDirection != 0)
		{
			UpdateBodySegments(_lastPosition, ate);
		}
	}
	private bool CheckCollision(RayCast2D ray)
	{
		if (!ray.IsColliding())
		{
			return false;
		}
		//collision occured
		GodotObject obj = ray.GetCollider();
		if (obj is TileMap tileMap)
		{
			//kill player
			GetTree().Paused = true;
			GD.Print("Snake hit a wall");
		}
		else if(obj is BodyPart bodyPart)
		{
			//kill player
			GetTree().Paused = true;
			GD.Print("Snake hit it's self");
		}
		else
		{
			//hit a fruit or so snake body
			score++;
			GD.Print("Snake ate an apple! Score: " + score);
			Random rand = new Random();
			_apple.SetGlobalPosition(new Vector2(16 * (rand.Next(2, 18)) - 8, 16 * rand.Next(2, 18) - 8));
			return true;
		}
		return false;
	}

	private void UpdateBodySegments(Vector2 lastPos, bool ate)
	{
		GD.Print(_snakeBodySegments.ToString() + $"Length: {_snakeBodySegments.Count}");
		if(_snakeBodySegments.Count == 0)
		{//add tail
			BodyPart bodyPart = GD.Load<PackedScene>("res://scenes//body_part.tscn").Instantiate<BodyPart>();
			GetNode<Node2D>("../").AddChild(bodyPart);
			bodyPart.Init(lastPos);
			bodyPart.setParent(this);
			_snakeBodySegments.AddFirst(bodyPart);

			bodyPart.updateSprite();
		}
		if(ate)
		{
			BodyPart firstPart = GD.Load<PackedScene>("res://scenes//body_part.tscn").Instantiate<BodyPart>();
			GetNode<Node2D>("../").AddChild(firstPart);
			
			firstPart.Init(lastPos);
			firstPart.setChild(_snakeBodySegments.First<BodyPart>());
			firstPart.setParent(this);
			_snakeBodySegments.First<BodyPart>().setParent(firstPart);
			_snakeBodySegments.AddFirst(firstPart);

			firstPart.updateSprite();
		}
		else if(_snakeBodySegments.Count == 1)
		{//has only a tail
			_snakeBodySegments.First<BodyPart>().Init(lastPos);

			_snakeBodySegments.First<BodyPart>().updateSprite();
		}
		else
		{//has 2 or more segments
			//Remove Tail
			BodyPart lastPart = _snakeBodySegments.Last<BodyPart>();
			_snakeBodySegments.RemoveLast();
			_snakeBodySegments.Last<BodyPart>().setChild(null);
			_snakeBodySegments.Last<BodyPart>().updateSprite();//update to now have tail
			lastPart.setParent(null);

			_snakeBodySegments.Last<BodyPart>().updateSprite();

			//Free Segment "lastPart" place as first body segment
			lastPart.Init(lastPos);
			lastPart.setChild(_snakeBodySegments.First<BodyPart>());
			lastPart.setParent(this);
			_snakeBodySegments.First<BodyPart>().setParent(lastPart);
			_snakeBodySegments.AddFirst(lastPart);

			lastPart.updateSprite();
		}
	}
}
