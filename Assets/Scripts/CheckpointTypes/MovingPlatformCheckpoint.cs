using UnityEngine;

public class MovingPlatformCheckpoint : Checkpoint
{
    [SerializeField] MovingPlatform platform;
    public override void NewConnection()
    {
        base.NewConnection();
        platform.MoveToEnd();
    }
}
