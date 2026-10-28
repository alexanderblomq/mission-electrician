using UnityEngine;

public class SimpleCheckpoint : Checkpoint
{
    protected override void OnConnected(Checkpoint prev)
    {
        GetComponent<AudioSource>().Play();
    }
}
