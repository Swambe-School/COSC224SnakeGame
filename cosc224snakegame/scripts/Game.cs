using Godot;
using System;

public partial class Game : Node2D
{
	public void _on_snake_tree_exited()
	{
		//ELLIS TESTS CASE #2 - when snake dies go to game over screen
		GD.Print("Send Player to game over scren");
		
		Node scene = ResourceLoader.Load<PackedScene>("res://scenes/game_over.tscn").Instantiate();
		GetTree().Root.AddChild(scene); 
		
		//ELLIS TEST CASE #4 - send score to the game over screen
		if(GameController.getInstance().getScore() != null)
		{
			GD.Print("Sending Score to game over screen");
		}
		
		GameOver tempScene = (GameOver) scene;
		tempScene.setFinalScore(GameController.getInstance().getScore());
		
		GetNode<Camera2D>("Camera2D").Enabled = false;
		this.QueueFree();
	}
}
