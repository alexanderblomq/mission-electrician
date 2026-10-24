using UnityEngine;

/* 
 * This is the base Checkpoint class, each node in the puzzle is a Checkpoint. This is an abstract class as some checkpoints have special behavior.
 */
public abstract class Checkpoint : MonoBehaviour
{
    private PlayerController playerController;
    protected bool connectable = true;

    abstract void Start()
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

    abstract void Update()
    {
        if (connectable && playerController && Input.GetKeyDown(KeyCode.E))
        {
            CableManager cable = playerController.GetComponentInChildren<CableManager>();
            NewConnection(cable.GetCurrentlyConnectedCheckpoint());
            cable.MarkCheckpoint(this);
        }
    }

    public abstract void NewConnection(Checkpoint prev) {
        GetComponent<SpriteRenderer>().color = Color.green;
        connectable = false;
    }

    public bool CheckPowered()
    {
        return powered;
    }
}
