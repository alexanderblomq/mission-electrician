using UnityEngine;

public class LightCheckpoint : Checkpoint
{
    public override void NewConnection(Checkpoint prev)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<Light>().TurnOn();
        powered = true;

        LevelManager.Instance.CheckWin();
    }
}
