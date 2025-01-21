using Godot;
using System;
using System.Formats.Asn1;

[Tool]
public partial class TitleEffects : Control
{
	private float rotationSpeed = 0.05f;
	private int rotationDirection = 1;

	private Random random = new Random();
	private int left = 0;
	private int right = 0;

	private float sizeSpeed= 1f;
	private double max = 1.1;
	private double min = 1.1;
	private float sizeModifier = 1.0005f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PivotOffset = GetSize() / 2;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//when random degree is hit switch
		if(RotationDegrees > right && rotationDirection == 1){
			rotationDirection = -1;
			right = random.Next(1,15);
		}//other way
		if(RotationDegrees < left && rotationDirection == -1){
			rotationDirection = 1;
			left = random.Next(-15, -1);
		}

		//flip between growing and shrinking
		if(Scale.X > max && sizeModifier  > 1){
			max = 1 + (random.NextDouble() / 9);
			sizeModifier = 0.9995f;
		}
		if(Scale.X < min && sizeModifier < 1){
			min = 1 - (random.NextDouble()/9);
			sizeModifier = 1.0005f;
		}

		//rotate
		Rotation +=  rotationDirection * rotationSpeed * (float)delta;
		//Scale
		Scale *= sizeModifier;

		
	}
}
