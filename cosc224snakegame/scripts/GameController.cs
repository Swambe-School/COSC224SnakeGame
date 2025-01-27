using Godot;
using System;

public partial class GameController : Node2D
{
	private int score;
	private static GameController instance;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = new GameController();
		instance.setScore(0);
	}
	
	public static GameController getInstance(){
		if(instance == null){
			instance = new GameController();
		}
		return instance;
	}
	
	public int getScore(){
		return score;
	}

	public void setScore(int score){
		this.score = score;
	}
	
	public void addPoint(){
		score++;
	}
}
