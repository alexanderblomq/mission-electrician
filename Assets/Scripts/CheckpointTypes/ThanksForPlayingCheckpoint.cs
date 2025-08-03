using UnityEngine;
using DG.Tweening;

public class ThanksForPlayingCheckpoint : Checkpoint
{
    [SerializeField] Animator animator;
    public override void NewConnection(Checkpoint prev)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<AudioSource>().Play();
        Color goal = new Color(0, 255, 108);
        animator.SetTrigger("Glow");
        powered = true;

        LevelManager.Instance.GameWon();
    }
}
