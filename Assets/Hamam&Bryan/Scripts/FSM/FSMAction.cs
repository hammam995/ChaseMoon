using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAction
{
    private readonly FSMState owner;

    public FSMAction (FSMState owner)
    {
        this.owner = owner;
    }
    public FSMState GetOwner()
    {
        return owner;
    }

    ///<summary>
    /// Starts the action
    ///</summary>
    public virtual void OnEnter()
    {
    }

    /// <summary>
    /// Updates the action in Update
    /// </summary>
    public virtual void OnUpdate()
    {
    }
    /// <summary>
    /// Updates the action in FiixedUpdate
    /// </summary>
    public virtual void OnFixedUpdate()
    {
    }
    /// <summary>
    /// Finishes the action
    /// </summary>
    public virtual void OnExit()
    {
    }
}
