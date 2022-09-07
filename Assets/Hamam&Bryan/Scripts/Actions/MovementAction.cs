using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : FSMAction
{
    private Transform playerTransform;
    private float speedTransition;
    public MovementAction(FSMState owner) : base(owner) { }
    public void SetSpeed(float speedTransition)
    {
        this.speedTransition = speedTransition;
    }
    public void Init(Transform playerTransform, float speedTransition)
    {
        this.playerTransform = playerTransform;
        this.speedTransition = speedTransition;
    }
    public override void OnEnter()
    {
        // Change animation to Walk
        Debug.Log("MOVEMENT ACTION");
    }
    public override void OnFixedUpdate()
    {
        // Action
        playerTransform.Translate(Input.GetAxis("Horizontal") * Vector2.right * speedTransition * 0.02f);
    }
}