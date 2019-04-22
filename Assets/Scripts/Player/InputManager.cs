using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static KeyboardManager KM;

    private IDevice[] devices;

    void Start()
    {
        devices = new IDevice[1]
        {
           KM
        };
    }

    public bool IsLeft()
    {
        foreach (var device in devices)
        {
            if (device.IsLeft())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsRight()
    {
        foreach (var device in devices)
        {
            if (device.IsRight())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsJump()
    {
        foreach (var device in devices)
        {
            if (device.IsJump())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsRun()
    {
        foreach (var device in devices)
        {
            if (device.IsRun())
            {
                return true;
            }
        }

        return false;
    }
    public bool IsDash()
    {
        foreach (var device in devices)
        {
            if (device.IsDash())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsAttack()
    {
        foreach (var device in devices)
        {
            if (device.IsAttack())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsGroundSmash()
    {
        foreach (var device in devices)
        {
            if (device.IsGroundSmash())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsCheckpoint()
    {
        foreach (var device in devices)
        {
            if (device.IsCheckpoint())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsInteraction()
    {
        foreach (var device in devices)
        {
            if (device.IsInteraction())
            {
                return true;
            }
        }

        return false;
    }
}

