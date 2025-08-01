using UnityEngine;

public class StartCheckpoint : Checkpoint
{
    public override void NewConnection(Checkpoint prev)
    {
        powered = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        LevelManager.Instance.CheckWin();
    }
}
