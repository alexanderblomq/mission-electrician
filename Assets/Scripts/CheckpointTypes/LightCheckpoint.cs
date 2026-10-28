using UnityEngine;

public class LightCheckpoint : Checkpoint
{
    protected override void OnConnected(Checkpoint prev)
    {
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<Light>().TurnOn();
    }
}
