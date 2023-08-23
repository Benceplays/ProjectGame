using Godot;
using System;

public partial class Menu : Node2D
{
    //valtozok
    double time = 50;
    double sec = 0;
    double min = 0;
    string second;
    string minute;
    //nodeok
    Label timeLabel;
    public override void _Ready()
    {
        timeLabel = GetNode("PlayButton/TimeLabel") as Label;
    }
    public override void _Process(double delta)
    {
        time += delta * 1.0;
        if (time/59 == 1) { min++; time = 0; GD.Print("most"); }
        else { sec = time; }
        if (sec < 10) { second = $"0{(int)sec}"; } else { second = $"{(int)sec}"; }
        if (min < 10) { minute = $"0{(int)min}"; } else { minute = $"{(int)min}"; }
        timeLabel.Text = $"{minute}:{second}";
    }
}
