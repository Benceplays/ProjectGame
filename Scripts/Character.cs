using Godot;
using System;

public partial class Character : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;
    private Vector2 _lastMousePos;
    private float _sensitivity = 0.005f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    public override void _Ready()
    {
        _lastMousePos = GetViewport().GetMousePosition();
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

        Vector2 currentMousePos = GetViewport().GetMousePosition();
        Vector2 mouseDelta = currentMousePos - _lastMousePos;
        _lastMousePos = currentMousePos;

        RotateX(-mouseDelta.Y * _sensitivity);
        RotateY(-mouseDelta.X * _sensitivity);
    }
}
