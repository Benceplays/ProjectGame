using Godot;
using System;

public partial class AllVariables : Node
{
    private static bool pausemenuison = false;
    public bool pausemenuon
    {
        get { return pausemenuison; }
        set { pausemenuison = value; }
    }
}
