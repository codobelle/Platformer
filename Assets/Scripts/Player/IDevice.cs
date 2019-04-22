using UnityEngine;

public interface IDevice
{
    KeyCode LeftKey { get; set; }
    KeyCode RightKey { get; set; }
    KeyCode JumpKey { get; set; }
    KeyCode RunKey { get; set; }
    KeyCode DashKey { get; set; }
    KeyCode AttackKey { get; set; }
    KeyCode GroundSmashKey { get; set; }
    KeyCode CheckpointKey { get; set; }
    KeyCode InteractionKey { get; set; }

    bool IsLeft();
    bool IsRight();
    bool IsJump();
    bool IsRun();
    bool IsDash();
    bool IsAttack();
    bool IsGroundSmash();
    bool IsCheckpoint();
    bool IsInteraction();
}
