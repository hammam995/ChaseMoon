using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAction : FSMAction
{
    private Rigidbody2D characterRigidbody;
    private float smashForce;
    private int numSmash;
    private bool canSmash;
    private bool isSmashing;
    private bool characterSide;
    private BoxCollider2D characterCollider;
    public SmashAction(FSMState owner): base(owner){ }
    public bool GetIsSmashing()
    {
        return isSmashing;
    }
    public void Init(float smashForce, bool characterSide, Rigidbody2D characterRigidbody, BoxCollider2D characterCollider)
    {
        this.smashForce = smashForce;
        this.characterRigidbody = characterRigidbody;
        this.characterSide = characterSide;
        this.characterCollider = characterCollider;
        numSmash = 1;
        canSmash = false;
    }
    public override void OnEnter()
    {
        numSmash = 1;
        // Set animation
    }
    public override void OnUpdate()
    {
        if(Input.GetButtonDown("Smash") && numSmash > 0)
        {
            canSmash = true;
            isSmashing = true;
            numSmash--;
        }
    }
    public override void OnFixedUpdate()
    {
        if(canSmash)
        {
            characterRigidbody.velocity = Vector2.zero;
            characterRigidbody.AddForce(Vector2.down * characterRigidbody.gravityScale * smashForce);
            canSmash = false;
        }
    }
    public override void OnExit()
    {
        numSmash = 1;
        isSmashing = false;
        canSmash = false;
    }
}
