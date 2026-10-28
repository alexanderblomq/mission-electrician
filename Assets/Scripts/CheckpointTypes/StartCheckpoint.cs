using UnityEngine;

public class StartCheckpoint : Checkpoint
{
    public override bool RequiredForCompletion => false;
    protected override bool CanConnect => base.CanConnect && LevelManager.Instance.isLevelWon();

    protected override void OnConnected(Checkpoint prev)
    {
        LevelManager.Instance.LevelWon();
    }
}
