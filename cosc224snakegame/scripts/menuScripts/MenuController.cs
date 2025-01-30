using Godot;
using System;

public partial class MenuController : Node
{
	private Camera2D camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera = GetNode<Camera2D>("MainCam");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_play_button_pressed()
	{
		//ELLIS TEST CASE #1 - if the menu button is properly hooked up, Print "Menu Button Test".
		GD.Print("Menu Button Test");
		
		Node scene = ResourceLoader.Load<PackedScene>("res://scenes/game.tscn").Instantiate();
		GetTree().Root.AddChild(scene); 
		camera.Enabled = false;
		this.QueueFree();
	}
	public void _on_quit_button_pressed()
	{
		//ELLIS TEST CASE #5 - if the Quit button is properly hooked up, Print "Quit Button Test".
		GD.Print("Quit Button Test");
		
		GetTree().Quit();
	}
}
