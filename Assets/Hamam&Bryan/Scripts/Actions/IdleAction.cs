using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : FSMAction
{
    public IdleAction (FSMState owner): base(owner) { }
    public override void OnEnter()
    {
        // Set an animation
        Debug.Log("ON IDLE");
    }
}
