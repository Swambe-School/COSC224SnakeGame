using Godot;
using System;

public partial class BodyPart : Area2D
{
	private int dirTo, dirFrom; //1 = up, 2 = right, -1 = down, -2 = left
	private Node2D parent;
	private Node2D child;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		dirTo = -2;
		dirFrom = 2;
		parent = null;
		child = null;
	}
	public void Init(Vector2 position)
	{
		this.SetGlobalPosition(position);

	}
	public void setParent(Node2D p){
		this.parent = p;
	}
	public void setChild(Node2D p){
		this.child = p;
	}
	public Node2D GetBodyParent()
	{
		return parent;
	}
	public Node2D GetBodyChild()
	{
		return child;
	}
	public void updateDirections(){
		//get pos
		float myX = this.GlobalPosition.X;
		float myY = this.GlobalPosition.Y;
		float parentX = parent.GlobalPosition.X;
		float parentY = parent.GlobalPosition.Y;
		
		//set dirTo
		if(myX == parentX){
			if(parentY < myY){
				dirTo = 1;
			}else{
				dirTo = -1;
			}
		}else{
			if(parentX < myX){
				dirTo = -2;
			}else{
				dirTo = 2;
			}
		}
		if(child != null){
			//set dirFrom
			float childX = child.GlobalPosition.X;
			float childY = child.GlobalPosition.Y;
			if(myX == childX){
				if(childY < myY){
					dirFrom = 1;
				}else{
					dirFrom = -1;
				}
			}else{
				if(childX < myX){
					dirFrom = -2;
				}else{
					dirFrom = 2;
				}
			}
			
		}
	}
	public void updateSprite(){
		
		updateDirections();
		
		AnimatedSprite2D mySprite = GetNode<AnimatedSprite2D>("BodySprite");
		//JESSE TEST CASE #1 - make sure a bodySegment is not coming from and going to the same direction
		if(dirTo == dirFrom){
			dirFrom = -dirTo;
		}
		//check dirFrom and dirTo to update to proper sprite
		if(child == null){
			//this is the tail
			//JESSE TEST CASE # 2 - make sure its showing a tail sprite
			if(mySprite.Frame != 0){
				mySprite.Frame = 0;
			}

			switch(dirTo){
				case 1: //heading up, tail should point down
					mySprite.Rotation = 0;
					break;
				case 2:
					mySprite.Rotation = Mathf.Pi * 0.5f;
					break;
				case -1:
					mySprite.Rotation = Mathf.Pi;
					break;
				case -2:
					mySprite.Rotation = Mathf.Pi * -0.5f;
					break;
				default:
					break;
			}
		}else{
			//this is a body part
			
			if(dirTo == -dirFrom){
				//this is a straight segment
				//JESSE TEST CASE # 3 - make sure its showing a body sprite
				if(mySprite.Frame != 5){
					mySprite.Frame = 5;
				}
				if(Mathf.Abs(dirTo) == 1){
					mySprite.Rotation = 0;
				}else{
					mySprite.Rotation = Mathf.Pi * 0.5f;
				}
			}else{
				//this is a kinky segment
				//JESSE TEST CASE # 4 - make sure its showing a kinked body sprite
				if(mySprite.Frame != 1){
					mySprite.Frame = 1;
				}
				//UP <-> RIGHT
				if(dirFrom == 1 || dirTo == 1){
					if(dirFrom == 2 || dirTo == 2){
						mySprite.Rotation = Mathf.Pi * -0.5f;
					}
				}
				//RIGHT <-> DOWN
				if(dirFrom == -1 || dirTo == -1){
					if(dirFrom == 2 || dirTo == 2){
						mySprite.Rotation = 0;
					}
				}
				//DOWN <-> LEFT
				if(dirFrom == -1 || dirTo == -1){
					if(dirFrom == -2 || dirTo == -2){
						mySprite.Rotation = Mathf.Pi * 0.5f;
					}
				}
				//LEFT <-> UP
				if(dirFrom == 1 || dirTo == 1){
					if(dirFrom == -2 || dirTo == -2){
						mySprite.Rotation = Mathf.Pi;
					}
				}
				
			}
			
		}
		//JESSE TEST CASE 5 - MAKE SURE ROTATION OBEYS OUR 4 DIRECTIONS
		if((mySprite.Rotation != 0) && (mySprite.Rotation != Mathf.Pi) && (mySprite.Rotation != Mathf.Pi * 0.5f) && (mySprite.Rotation != Mathf.Pi * -0.5f)){
			updateSprite();
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
