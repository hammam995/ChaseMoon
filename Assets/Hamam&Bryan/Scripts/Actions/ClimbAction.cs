using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAction : FSMAction
{
    private float speed;
    private float thresholdY;
    private string finishEvent;
    private bool characterUpOrDown;
    private Transform characterTransform;
    private Rigidbody2D characterRigidbody;
    private Collider2D characterCollider;
    private Collider2D ladderCollider;
    private MainCharacterFSM mcFsm;
    public ClimbAction (FSMState owner): base(owner){}
    public void SetLadderCollider(Collider2D ladderCollider)
    {
        this.ladderCollider = ladderCollider;
    }
    public void Init(float speed, float thresholdY, bool characterUpOrDown, Rigidbody2D characterRigidbody, Collider2D characterCollider, Transform characterTransform, MainCharacterFSM mcFsm, string finishEvent)
    {
        this.speed = speed;
        this.thresholdY = thresholdY;
        this.characterUpOrDown = characterUpOrDown;
        this.characterRigidbody = characterRigidbody;
        this.characterCollider = characterCollider;
        this.characterTransform = characterTransform;
        this.mcFsm = mcFsm;
        this.finishEvent = finishEvent;
    }
    public override void OnEnter()
    {
        characterRigidbody.velocity = Vector2.zero;
        characterRigidbody.bodyType = RigidbodyType2D.Kinematic;
        characterCollider.isTrigger = true;
    }
    public override void OnFixedUpdate()
    {
        float verticalAxis;
        verticalAxis = Input.GetAxis("Vertical");
        if(IsInTop() && verticalAxis > 0)
        {
            mcFsm.SetOnAir(false);
            FinishEvent(finishEvent);
            return;
        } else if(IsInBottom() && verticalAxis < 0)
        {
            mcFsm.SetOnAir(false);
            FinishEvent(finishEvent);
            return;
        } else
        {
            mcFsm.SetOnAir(true);
            characterTransform.Translate(verticalAxis * Vector2.up * characterRigidbody.gravityScale * speed * 0.02f);
        }
    }
    public override void OnExit()
    {
        characterRigidbody.bodyType = RigidbodyType2D.Dynamic;
        characterCollider.isTrigger = false;
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
    public void FinishEvent(string finishEvent)
    {
        if(finishEvent != null)
            GetOwner().SendEvent(finishEvent);
    }
}
