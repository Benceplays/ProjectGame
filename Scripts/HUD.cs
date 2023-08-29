using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private Panel pausepanel;
	private Panel exitpanel;
	public override void _Ready()
	{
        pausepanel = GetNode("PausePanel") as Panel;
        exitpanel = GetNode("ExitPanel") as Panel;
    }
	public void ResumeButton() { pausepanel.Visible = false; }
	public void ExitButton() { exitpanel.Visible = true; }
	public void CancelButton() { exitpanel.Visible = false; }
	public void SureButton() { pausepanel.Visible = false; exitpanel.Visible = false; GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn"); }
    public override void _Process(double delta)
	{
	}
}
