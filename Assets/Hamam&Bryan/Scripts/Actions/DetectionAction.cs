using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAction: FSMAction
{
    private float radius;
    private string finishEvent;
    private bool upOrDown;
    private Transform enemyTransform;
    public DetectionAction (FSMState owner): base(owner) { }
    public void Init(float radius, bool upOrDown, Transform enemyTransform, string finishEvent)
    {
        this.radius = radius;
        this.upOrDown = upOrDown;
        this.enemyTransform = enemyTransform;
        this.finishEvent = finishEvent;
    }
    public override void OnEnter()
    {

    }
    public override void OnFixedUpdate()
    {
        DetectPlayer();
    }
    public override void OnExit()
    {

    }
    private void DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyTransform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                MainCharacterFSM playerFSM = collider.GetComponent<MainCharacterFSM>();
                if (playerFSM.GetCharacterUpOrDown() == upOrDown)
                {
                    FinishEvent(finishEvent);
                    return;
                }
            }
        }
    }
    private void FinishEvent(string finishEvent)
    {
        if (finishEvent != null)
            GetOwner().SendEvent(finishEvent);
    }
}
