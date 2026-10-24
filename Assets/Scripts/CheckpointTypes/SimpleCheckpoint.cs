using UnityEngine;

public class SimpleCheckpoint : Checkpoint
{
    public override void NewConnection()
    {
        base.NewConnection();
        GetComponent<AudioSource>().Play();
    }
}
