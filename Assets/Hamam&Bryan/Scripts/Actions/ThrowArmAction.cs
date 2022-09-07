using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowArmAction : FSMAction
{
    private float speed;
    private float time;
    private float heightParabola;
    private bool inTransition = false;
    private Vector2 startParabola;
    private Vector2 pivotPos;
    private Vector2 endParabola;
    private Rigidbody2D characterRigidbody;
    private Transform characterTransform;
    private MainCharacterFSM mcFsm;
    public ThrowArmAction(FSMState owner): base(owner) {}
    public bool GetInTransition()
    {
        return inTransition;
    }
    public void SetHeightParabola(float heightParabola)
    {
        this.heightParabola = heightParabola;
    }
    public void SetStartParabola(Vector2 startParabola)
    {
        this.startParabola = startParabola;
    }
    public void SetPivotPosition(Vector2 pivotPos)
    {
        this.pivotPos = pivotPos;
    }
    public void SetEndParabola(Vector2 endParabola)
    {
        this.endParabola = endParabola;
    }
    public void Init(float speed, Rigidbody2D characterRigidbody, Transform characterTransform)
    {
        this.speed = speed;
        this.characterRigidbody = characterRigidbody;
        this.characterTransform = characterTransform;
        mcFsm = characterTransform.GetComponent<MainCharacterFSM>();
    }
    public override void OnEnter()
    {
        Debug.Log("THROWING ARM");
        mcFsm.SetOnAir(true);
        time = 0;
        inTransition = true;
        characterRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }
    public override void OnFixedUpdate()
    {
        time += Time.fixedDeltaTime;
        characterTransform.position = Parabola(startParabola, endParabola, heightParabola, time * speed);
        if(time * speed > 0.6f && characterTransform.position.y * characterRigidbody.gravityScale < pivotPos.y * characterRigidbody.gravityScale)
        {
            characterRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public override void OnExit()
    {
        mcFsm.SetOnAir(false);
        inTransition = false;
        characterRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    private Vector2 Parabola(Vector2 start, Vector2 end, float height, float time)
    {
        float y;
        y = (-4f * height * time * time) + (4f * height * time);
        Vector2 point = Vector2.Lerp(start, end, time);
        return new Vector2(point.x, y + Mathf.Lerp(start.y,end.y,time));
    }
}
