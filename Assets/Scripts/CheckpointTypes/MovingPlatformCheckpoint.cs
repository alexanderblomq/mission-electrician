using UnityEngine;

public class MovingPlatformCheckpoint : Checkpoint
{
    [SerializeField] MovingPlatform platform;
    public override void NewConnection(Checkpoint prev)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        powered = true;
        platform.MoveToEnd();

        LevelManager.Instance.CheckWin();
    }

    //public override void Disconnected()
    //{
    //    powered = false;
    //}
}
