using Godot;
using Microsoft.Identity.Client;
using System;

public partial class Settings : Node2D
{
	private Label soundLabel;
	private HSlider soundSlider;
	private Label musicLabel;
	private HSlider musicSlider;
	private OptionButton displaymodeOption;


    private string path;
    private ConfigFile config;
    public int displayModeId;
    public int resolutionId;
    public override void _Ready()
	{
        soundLabel = GetNode("SettingsPanel/AudioLabel/SoundSlider/SoundLabelValue") as Label;
		soundSlider = GetNode("SettingsPanel/AudioLabel/SoundSlider") as HSlider;
		musicLabel = GetNode("SettingsPanel/AudioLabel/MusicSlider/MusicLabelValue") as Label;
		musicSlider = GetNode("SettingsPanel/AudioLabel/MusicSlider") as HSlider;
        displaymodeOption = GetNode("SettingsPanel/GameLabel/DisplayModeLabel/DisplayModeOption") as OptionButton;

		//Option feltoltes
        displaymodeOption.AddItem("Windowed");
        displaymodeOption.AddItem("Fullscreen");
        displaymodeOption.AddItem("Borderless Windowed");


        //Filebol betoltes a frontend reszre
        path = "res://Options.cfg"; // res ha szamitogepes vagy user ha telefonra keszul
        config = new ConfigFile();
        config.Load(path);
        soundSlider.Value = (float)config.GetValue("Audio", "Sound");
        musicSlider.Value = (float)config.GetValue("Audio", "Music");
        displaymodeOption.Selected = (int)config.GetValue("Game", "DisplayMode", 0);
    }
    public void DisplayModeSelected(int id)
    {
        switch (id)
        {
            case 0:
                displayModeId = 0;
                break;
            case 1:
                displayModeId = 1;
                break;
            case 2:
                displayModeId = 2;
                break;
        }
    }
    public void SaveButton()
    {
        config.SetValue("Audio", "Sound", soundSlider.Value);
        config.SetValue("Audio", "Music", musicSlider.Value);
        config.SetValue("Game", "DisplayMode", displayModeId);
        config.Save(path);
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }
    public override void _Process(double delta)
	{
		soundLabel.Text = $"{soundSlider.Value}%";
		musicLabel.Text = $"{musicSlider.Value}%";
	}
}

