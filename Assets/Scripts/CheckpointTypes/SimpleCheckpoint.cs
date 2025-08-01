using UnityEngine;

public class SimpleCheckpoint : Checkpoint
{
    public override void NewConnection(Checkpoint prev)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        powered = true;

        LevelManager.Instance.CheckWin();
    }
}
