using UnityEngine;

public abstract class Checkpoint : MonoBehaviour
{
    protected bool powered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelManager.Instance.RegisterCheckpoint(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CableManager cableManager = collision.gameObject.GetComponent<CableManager>();
        if(!powered && cableManager)
        {
            NewConnection(cableManager.GetCurrentlyConnectedCheckpoint());
            cableManager.MarkCheckpoint(this);
        }
    }

    public abstract void NewConnection(Checkpoint prev);

    public bool CheckPowered()
    {
        return powered;
    }
}