
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacterAction : FSMAction
{
    private float minDistanceX;
    private float time;
    private string toMovement;
    private string toIdle;
    private MainCharacterFSM thisCharacter;
    private MainCharacterFSM otherCharacter;
    public ControlCharacterAction (FSMState owner): base(owner) { }
    public void Init(float minDistanceX, MainCharacterFSM thisCharacter, MainCharacterFSM otherCharacter, string toMovement, string toIdle)
    {
        this.minDistanceX = minDistanceX;
        this.thisCharacter = thisCharacter;
        this.otherCharacter = otherCharacter;
        this.toMovement = toMovement;
        this.toIdle = toIdle;
    }
    // SE DEBE DE MANTENER EL BOTON PULSADO
    public override void OnUpdate()
    {
        float distanceX;
        distanceX = Mathf.Abs(thisCharacter.transform.position.x - otherCharacter.transform.position.x);
        if (Input.GetButtonDown("Change Character") && thisCharacter.GetCharacterUpOrDown())
        {
            if(thisCharacter.onControl && otherCharacter.onControl && !thisCharacter.GetOnAir())
            {
                Debug.Log("SEPARAOS");
                otherCharacter.onControl = false;
                if(!otherCharacter.ThrowArm.GetInTransition())
                    otherCharacter.GetCurrentState().SendEvent(toIdle);
                return;
            } else if(distanceX > minDistanceX)
            {
                Debug.Log("CAMBIA AL OTRO PERSONAJE");
                if (thisCharacter.onControl && !otherCharacter.onControl)
                {
                    ChangeCharacters(thisCharacter, otherCharacter);
                    return;
                }
                if (otherCharacter.onControl && !thisCharacter.onControl)
                {
                    ChangeCharacters(otherCharacter, thisCharacter);
                    return;
                }
            } else 
            {
                Debug.Log("PUEDE UNIRSE");
                if(thisCharacter.onControl && !thisCharacter.GetOnAir() && !thisCharacter.onTransitionUnion)
                {
                    thisCharacter.targetPosTransition = new Vector2(otherCharacter.transform.position.x, thisCharacter.transform.position.y);
                    thisCharacter.GetCurrentState().SendEvent(toIdle);
                    thisCharacter.onTransitionUnion = true;
                    otherCharacter.onTransitionUnion = false;
                    time = 0;
                    return;
                } else if(otherCharacter.onControl && !otherCharacter.GetOnAir() && !otherCharacter.onTransitionUnion)
                {
                    Debug.Log("ALINEANDOSE CON EL NEGRO");
                    otherCharacter.targetPosTransition = new Vector2(thisCharacter.transform.position.x, otherCharacter.transform.position.y);
                    otherCharacter.GetCurrentState().SendEvent(toIdle);
                    otherCharacter.onTransitionUnion = true;
                    thisCharacter.onTransitionUnion = false;
                    time = 0;
                    return;
                }
                //Set animation walk
            }
        } 
    }
    public override void OnFixedUpdate()
    {
        if(thisCharacter.GetCharacterUpOrDown())
        {
            if (thisCharacter.onControl && thisCharacter.onTransitionUnion)
            {
                MoveToOtherCharacter(thisCharacter, otherCharacter, thisCharacter.targetPosTransition);
                return;
            }
            if (otherCharacter.onControl && otherCharacter.onTransitionUnion)
            {
                Debug.Log("MOVIENDO EL BLANCO");
                MoveToOtherCharacter(otherCharacter, thisCharacter, otherCharacter.targetPosTransition);
                return;
            }
            if (thisCharacter.onControl && otherCharacter.onControl)
            {
                float distanceX;
                distanceX = Mathf.Abs(thisCharacter.transform.position.x - otherCharacter.transform.position.x);
                if (distanceX > 0.1f && !thisCharacter.GetOnAir())
                {
                    Vector2 targetPos;
                    targetPos = new Vector2(otherCharacter.transform.position.x, thisCharacter.transform.position.y);
                    MoveToOtherCharacter(thisCharacter, otherCharacter, targetPos);
                }
            }
        }
    }
    private void MoveToOtherCharacter(MainCharacterFSM fromCharacter, MainCharacterFSM toCharacter, Vector2 targetPosition)
    {
        Vector2 currentTargetPos;
        currentTargetPos = new Vector2(targetPosition.x, fromCharacter.transform.position.y);
        if (Vector2.Distance(fromCharacter.transform.position, currentTargetPos) > 0.01f)
        {
            time += Time.fixedDeltaTime;
            fromCharacter.transform.position = Vector2.Lerp(fromCharacter.transform.position, currentTargetPos, time);
        } else
        {
            Debug.Log(fromCharacter.GetCurrentState().Name);
            Debug.Log(toCharacter.GetCurrentState().Name);
            fromCharacter.onControl = true;
            toCharacter.onControl = true;
            time = 0;
            fromCharacter.onTransitionUnion = false;
            toCharacter.onTransitionUnion = false;
            fromCharacter.GetIdleState().SendEvent(toMovement);
            toCharacter.GetIdleState().SendEvent(toMovement);
        }
    }
    private void ChangeCharacters(MainCharacterFSM fromCharacter, MainCharacterFSM toCharacter)
    {
        if (fromCharacter.onControl && !toCharacter.onControl)
        {
            Debug.Log("CAMBIA PERSONAJES");
            fromCharacter.onControl = false;
            toCharacter.onControl = true;
            SetCharactersState(fromCharacter, toCharacter);
        }
    }
    private void SetCharactersState(MainCharacterFSM fromCharacter, MainCharacterFSM toCharacter)
    {
        toCharacter.GetCurrentState().SendEvent(toMovement);
        if (!fromCharacter.ThrowArm.GetInTransition())
            fromCharacter.GetCurrentState().SendEvent(toIdle);
    }
}
