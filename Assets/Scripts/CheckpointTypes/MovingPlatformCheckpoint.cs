using UnityEngine;

public class MovingPlatformCheckpoint : Checkpoint
{
    public override void NewConnection(Checkpoint prev)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        powered = true;
        GetComponentInParent<MovingPlatform>().MoveToEnd();

        LevelManager.Instance.CheckWin();
    }

    //public override void Disconnected()
    //{
    //    powered = false;
    //}
}
