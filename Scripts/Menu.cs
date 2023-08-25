using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Reflection.PortableExecutable;

public partial class Menu : Node2D
{
    //valtozok
    static string dbserver = "127.0.0.1";
    static string database = "projectgame";
    static string dbusername = "user";
    static string dbpassword = "joWAyE";
    static MySqlConnection conn;
    static MySqlDataReader reader;
    bool SearchIsOn = false;
    double time = 0;
    double sec = 0;
    double min = 0;
    string second;
    string minute;
    //nodeok valtozoi
    Label timeLabel;
    public override void _Ready()
    {
        timeLabel = GetNode("PlayButton/TimeLabel") as Label;
        new Machmaking();
    }
    public override void _Process(double delta)
    {
        if (SearchIsOn)
        {
            time += delta * 1.0;
            if (time >= 60) { min++; time = 0; } else { sec = time; }
            if (sec < 10) { second = $"0{(int)sec}"; } else { second = $"{(int)sec}"; }
            if (min < 10) { minute = $"0{(int)min}"; } else { minute = $"{(int)min}"; }
            timeLabel.Text = $"{minute}:{second}";
        }
    }
    public void ServerSearchButton() { SearchIsOn = true; }
}
public class Machmaking
{
    static string dbserver = "127.0.0.1";
    static string database = "projectgame";
    static string dbusername = "user";
    static string dbpassword = "joWAyE";
    static MySqlConnection conn;
    static MySqlDataReader reader;
    static Machmaking()
    {
        GD.Print("asd");
        Connection();
    }
    public static void Connection()
    {
        string constring = $"SERVER={dbserver};DATABASE={database};UID={dbusername};PASSWORD={dbpassword};";
        Console.WriteLine("Elindult");
        conn = new MySqlConnection(constring);
        conn.Open();
    }
    public static void ServerSearch()
    {
        Connection();
        string query = $"SELECT * FROM servers";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        reader = cmd.ExecuteReader();
        reader.Read();
        Console.WriteLine("IP: " + reader["ip"] + " Current players: " + reader["currentplayers"] + " Max player: " + reader["maxplayer"]);
        reader.Close();
        Console.ReadKey();
    }
}