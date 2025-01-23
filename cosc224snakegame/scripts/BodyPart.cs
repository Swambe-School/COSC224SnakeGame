using Godot;
using System;

public partial class BodyPart : Node
{
	private int dirTo, dirFrom;
	private BodyPart parent;
	private BodyPart child;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		parent = null;
		child = null;
	}
	public void setParent(BodyPart p){
		this.parent = p;
	}
	public void setChild(BodyPart p){
		this.child = p;
	}
	public void updateSprite(){
		//check dirFrom and dirTo to update to proper sprite
		if(child == null){
			//this is the tail
			
		}else{
			//this is a body part
		}
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
