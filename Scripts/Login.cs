using Godot;
using MySql.Data.MySqlClient;
using System;

public partial class Login : Node2D
{
    static string dbserver = "ptero02.adminom.hu";
    static string database = "s4330_projectgame";
    static string dbusername = "u4330_XuXcbj6mwi";
    static string dbpassword = "A@oItlZZnE8=tlKSdyDo0S8Q";
    static MySqlDataReader reader;
    static MySqlConnection connection;
    
    public TextEdit usernameinput;
	private TextEdit passwordinput;
    public override void _Ready()
	{
		usernameinput = GetNode("UsernameInput") as TextEdit;
        passwordinput = GetNode("PasswordInput") as TextEdit;
    }
    public void LoginButton()
    {
        Connection();
    }
    public void Connection()
    {
        string connectionString = $"Server={dbserver};Database={database};Uid={dbusername};Pwd={dbpassword};";
        using (connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                GD.Print("Sikeres csatlakozas a MySQL szerverhez!");
                LoginTry();
            }
            catch (Exception ex)
            {
                GD.Print("Hiba tortent a csatlakozas soran: " + ex.Message);
            }
        }
    }
    public void LoginTry()
    {
        string userquery = $"SELECT * FROM users WHERE username='{Convert.ToString(usernameinput.Text)}'";
        MySqlCommand cmd = new MySqlCommand(userquery, connection);
        reader = cmd.ExecuteReader();
        while (reader.Read()) { 
            if (Convert.ToString(reader["password"]) == Convert.ToString(passwordinput.Text)) { GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn"); } else { GD.Print("Nem jou valami"); }
        }
        reader.Close();
        connection.Close();
    }
    public override void _Process(double delta)
	{
	}
}
