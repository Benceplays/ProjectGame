using Godot;
using System;

public partial class Character : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;
    private Vector2 _lastMousePos;
    private float _sensitivity = 0.005f;
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private const float MouseSensitivity = 0.1f;
    private Vector2 _mouseDelta;

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion mouseMotion)
        {
            _mouseDelta = mouseMotion.Relative;
        }
    }
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;

        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();

        RotateY(-_mouseDelta.X * MouseSensitivity);

        _mouseDelta = Vector2.Zero;
    }
}
