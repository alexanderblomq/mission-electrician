using UnityEngine;

public class LaserWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if(controller)
        {
            controller.Electrocute();
        }
    }
}
