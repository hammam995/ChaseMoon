using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackPlayer : FSMAction
{
    private Animator characterAnimator;
    private MainCharacterFSM mcfsm;
    public AttackPlayer(FSMState owner) : base(owner) { }
    public void Init(Animator characterAnimator, MainCharacterFSM mcfsm)
    {
        this.characterAnimator = characterAnimator;
    }
    public override void OnEnter()
    {
        // Change animation to Walk
        //characterAnimator.SetBool("Walking", true);

    }
    public override void OnExit()
    {

    }
}