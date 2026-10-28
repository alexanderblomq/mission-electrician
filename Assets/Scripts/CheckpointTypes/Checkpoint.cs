using UnityEngine;

/* 
 * This is the base Checkpoint class, each node in the puzzle is a Checkpoint. This is an abstract class as some checkpoints have special behavior.
 */
public abstract class Checkpoint : MonoBehaviour
{
    private PlayerController playerController;
    private bool powered;

    public virtual bool RequiredForCompletion => true;
    protected virtual bool CanConnect => !powered;

    private void Start()
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
        if (playerController && Input.GetKeyDown(KeyCode.E))
        {
            CableManager cable = playerController.GetComponentInChildren<CableManager>();
            TryConnect(cable);
        }
    }

    public bool TryConnect(CableManager cable)
    {
        if (!CanConnect || !cable)
        {
            return false;
        }

        Checkpoint prev = cable.GetCurrentlyConnectedCheckpoint();
        powered = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        cable.MarkCheckpoint(this);
        OnConnected(prev);
        return true;
    }

    protected virtual void OnConnected(Checkpoint prev)
    {
    }

    public bool CheckPowered()
    {
        return powered;
    }
}
