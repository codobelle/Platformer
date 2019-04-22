using UnityEngine;

public class KeyboardManager : IDevice
{
    //  properties
    public KeyCode LeftKey { get; set; }

    public KeyCode RightKey { get; set; }

    public KeyCode JumpKey { get; set; }

    public KeyCode RunKey { get; set; }

    public KeyCode DashKey { get; set; }

    public KeyCode AttackKey { get; set; }

    public KeyCode GroundSmashKey { get; set; }

    public KeyCode CheckpointKey { get; set; }

    public KeyCode InteractionKey { get; set; }

    //  methods
    public bool IsLeft()
    {
        return Input.GetKey(LeftKey);
    }

    public bool IsRight()
    {
        return Input.GetKey(RightKey);
    }

    public bool IsJump()
    {
        return Input.GetKeyDown(JumpKey);
    }

    public bool IsRun()
    {
        return Input.GetKey(RunKey);
    }

    public bool IsDash()
    {
        return Input.GetKeyDown(DashKey);
    }

    public bool IsAttack()
    {
        return Input.GetKeyDown(AttackKey);
    }

    public bool IsGroundSmash()
    {
        return Input.GetKeyDown(GroundSmashKey);
    }

    public bool IsCheckpoint()
    {
        return Input.GetKey(CheckpointKey);
    }

    public bool IsInteraction()
    {
        return Input.GetKeyDown(InteractionKey);
    }
}
