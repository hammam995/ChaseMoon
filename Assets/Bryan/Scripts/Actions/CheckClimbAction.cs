using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClimbAction : FSMAction
{
    private float thresholdY;
    private string finishEvent;
    private bool characterUpOrDown;
    private bool canClimb;
    private bool onAir;
    private Collider2D characterCollider;
    private Collider2D ladderCollider;
    public CheckClimbAction (FSMState owner): base(owner) {}
    public void SetCanClimb(bool canClimb)
    {
        this.canClimb = canClimb;
    }
    public void SetLadderCollider(Collider2D ladderCollider)
    {
        this.ladderCollider = ladderCollider;
    }
    public void SetOnAir(bool onAir)
    {
        this.onAir = onAir;
    }
    public void Init(float thresholdY, bool characterUpOrDown, bool onAir, Collider2D characterCollider, string finishEvent)
    {
        this.thresholdY = thresholdY;
        this.characterUpOrDown = characterUpOrDown;
        this.onAir = onAir;
        this.characterCollider = characterCollider;
        this.finishEvent = finishEvent;
    }
    public override void OnFixedUpdate()
    {
        float verticalAxis;
        verticalAxis = Input.GetAxis("Vertical");
        if(canClimb)
        {
            if(!onAir)
            {
                if(IsInTop() && verticalAxis < 0)
                {
                    Finish(finishEvent);
                    return;
                } else if(IsInBottom() && verticalAxis > 0)
                {
                    Finish(finishEvent);
                    return;
                }
            } else if(verticalAxis != 0)
            {
                Finish(finishEvent);
                return;
            }
        }
    }
    private bool IsInTop()
    {
        float maxY;
        maxY = characterUpOrDown ? (ladderCollider.bounds.max.y - thresholdY) : (ladderCollider.bounds.min.y + thresholdY);
        return characterUpOrDown ? (characterCollider.bounds.min.y > maxY) : (characterCollider.bounds.max.y < maxY);
    }
    private bool IsInBottom()
    {
        float minY;
        minY = characterUpOrDown ? (ladderCollider.bounds.min.y + thresholdY) : (ladderCollider.bounds.max.y - thresholdY);
        return characterUpOrDown ? (characterCollider.bounds.min.y < minY) : (characterCollider.bounds.max.y > minY);;
    }
    private void Finish(string finishEvent)
    {
        if(finishEvent != null)
            GetOwner().SendEvent(finishEvent);
    }
}
