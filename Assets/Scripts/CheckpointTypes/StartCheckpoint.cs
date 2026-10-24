using UnityEngine;

public class StartCheckpoint : Checkpoint
{
    public override void Start() {
      connectable = false;
    }

    public override void Update() {
      if(!LevelManager.Instance.isLevelWon()){
        return;
      }
      base.Update();
    }

    public override void NewConnection()
    {
        base.NewConnection();
        LevelManager.Instance.LevelWon();
    }
}
