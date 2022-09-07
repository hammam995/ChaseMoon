using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInteractable: MonoBehaviour, IInteractable
{
    [Header("Objects Settings")]
    [SerializeField] protected bool upOrDown;
    [SerializeField] protected bool characterCanMove;
    [Header("Unity Events")]
    [SerializeField] protected UnityEvent EnterAction;
    [SerializeField] protected UnityEvent ExitAction;
    [SerializeField] protected UnityEvent Action;

    public bool canMove => characterCanMove;
    public virtual float coefficientSpeed => 0f;

    private void Update()
    {
       
    }

    public virtual void Use() 
    {
        Action.Invoke();
    }
    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            EnterAction.Invoke();
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            ExitAction.Invoke();
        }
    }
}
