using UnityEngine;

public class LightCheckpoint : Checkpoint
{
    public override void NewConnection()
    {
        base.NewConnection();
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<Light>().TurnOn();
    }
}
