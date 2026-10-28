using UnityEngine;

public class MovingPlatformCheckpoint : Checkpoint
{
    [SerializeField] MovingPlatform platform;
    protected override void OnConnected(Checkpoint prev)
    {
        platform.MoveToEnd();
    }
}
