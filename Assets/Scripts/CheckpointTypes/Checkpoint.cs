using UnityEngine;

public abstract class Checkpoint : MonoBehaviour
{
    protected bool powered;
    private PlayerController playerController;

    void Start()
    {
        LevelManager.Instance.RegisterCheckpoint(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out playerController);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController = null;
    }

    private void Update()
    {
        if (!powered && playerController && Input.GetKeyDown(KeyCode.E))
        {
            CableManager cable = playerController.GetComponentInChildren<CableManager>();
            NewConnection(cable.GetCurrentlyConnectedCheckpoint());
            cable.MarkCheckpoint(this);
        }
    }

    public abstract void NewConnection(Checkpoint prev);

    public bool CheckPowered()
    {
        return powered;
    }
}