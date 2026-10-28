using UnityEngine;
using DG.Tweening;

public class ThanksForPlayingCheckpoint : Checkpoint
{
    [SerializeField] Animator animator;
    protected override void OnConnected(Checkpoint prev)
    {
        GetComponent<AudioSource>().Play();
        Color goal = new Color(0, 255, 108);
        animator.SetTrigger("Glow");

        LevelManager.Instance.GameWon();
    }
}
