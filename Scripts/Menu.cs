using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

public partial class Menu : Node2D
{
    //valtozok
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
    public void ServerSearchButton() { SearchIsOn = true; new Machmaking(); }
}
public class Machmaking
{
    public static int province = 0;

    static string dbserver = "ptero02.adminom.hu";
    static string database = "s4330_projectgame";
    static string dbusername = "u4330_XuXcbj6mwi";
    static string dbpassword = "A@oItlZZnE8=tlKSdyDo0S8Q";
    static MySqlDataReader reader;
    static MySqlConnection connection;
    static Machmaking()
    {
        Connection();
    }
    public static void Connection()
    {
        string connectionString = $"Server={dbserver};Database={database};Uid={dbusername};Pwd={dbpassword};";
        using (connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                GD.Print("Sikeres csatlakozas a MySQL szerverhez!");
            }
            catch (Exception ex)
            {
                GD.Print("Hiba tortent a csatlakozas soran: " + ex.Message);
            }
            ServerSearch();
        }
    }
    // Score lekerdezes
    public static int GetScore()
    {
        int score = 0;
        string username = "Booby";
        string userquery = $"SELECT * FROM users WHERE username='{username}'";
        MySqlCommand cmd = new MySqlCommand(userquery, connection);
        reader = cmd.ExecuteReader();
        reader.Read();
        score = Convert.ToInt32(reader["score"]);
        reader.Close();
        return score;
    }
    public static void ServerSearch()
    {
        // Score tartomany meghatarozas
        int VerifyScore = Convert.ToInt32(GetScore());
        if (VerifyScore > 0 && VerifyScore < 1000) { GD.Print(VerifyScore); province = 1; }
        else if (VerifyScore > 1000 && VerifyScore < 3000) { GD.Print(VerifyScore); province = 2; }
        else if (VerifyScore > 3000 && VerifyScore < 5000) { GD.Print(VerifyScore); province = 3; }
        else if (VerifyScore > 5000 && VerifyScore < 7000) { GD.Print(VerifyScore); province = 4; }
        else if (VerifyScore > 7000 && VerifyScore < 10000) { GD.Print(VerifyScore); province = 5; }
        else if (VerifyScore > 10000) { GD.Print(VerifyScore); province = 6; }

        GD.Print("-------------------------------------------------------");

        // Szerver lekerdezes
        List<string> goodservers = new List<string>();
        string serverquery = $"SELECT * FROM servers WHERE province='{province}'";
        MySqlCommand cmd = new MySqlCommand(serverquery, connection);
        reader = cmd.ExecuteReader();
        while (reader.Read()) { goodservers.Add(reader["ip"] + ";" + reader["currentplayers"] + ";" + reader["maxplayer"] + ";" + reader["province"]); }
        foreach (var item in goodservers) { GD.Print(item); }
        reader.Close();
        connection.Close();
    }
}