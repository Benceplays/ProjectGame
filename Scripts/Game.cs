using Godot;
using System;

public partial class Game : Node3D
{
	public CharacterBody3D maincharacter;
	public DirectionalLight3D sun;
	public override void _Ready()
	{
		maincharacter = GetNode("/root/Game/CharacterBody3D") as CharacterBody3D;
		sun = GetNode("Sun") as DirectionalLight3D;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		sun.LookAt(new Vector3(maincharacter.Position.X, maincharacter.Position.Y, maincharacter.Position.Z));
	}
}
