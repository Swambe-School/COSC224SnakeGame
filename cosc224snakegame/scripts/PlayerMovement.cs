using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{


	public override void _Process(double delta)
	{
		Vector2 direction = new Vector2(Input.GetAxis("right","left"),Input.GetAxis("down","up"));
		
		//ignore inputs that push into snake body
		
	}
}
