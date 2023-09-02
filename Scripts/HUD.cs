using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private Panel pausepanel;
	private Panel exitpanel;
	public PackedScene psoscirulos;
	public Node newNodeInstance;
	private AllVariables allVariables;
	public override void _Ready()
	{
		pausepanel = GetNode("PausePanel") as Panel;
		exitpanel = GetNode("ExitPanel") as Panel;
		allVariables = new AllVariables();

	}
	public void ResumeButton() { pausepanel.Visible = false; Input.MouseMode = Input.MouseModeEnum.ConfinedHidden; allVariables.pausemenuon = false; }
	public void ExitButton() { exitpanel.Visible = true; }
	public void CancelButton() { exitpanel.Visible = false; }
	public void SureButton() { pausepanel.Visible = false; exitpanel.Visible = false; GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn"); }
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("PauseMenu"))
		{
			pausepanel.Visible = true;
			allVariables.pausemenuon = true;
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		if (Input.IsActionJustPressed("TakeButtonE"))
		{
			GD.Print("E");
		}
		if (Input.IsActionJustPressed("TakeButtonQ"))
		{
			GD.Print("Q");
		}
		if (Input.IsActionJustPressed("TakeButtonX"))
		{
			GD.Print("X");
		}
		if (Input.IsActionJustPressed("TakeButtonY"))
		{
			GD.Print("Y");
		}
	}
}
