using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollAction: FSMAction
{
    private float speed;
    private float distance;
    private Transform enemyTransform;

    private Vector2 startPoint;
    private float limitMin;
    private float limitMax;
    private float direction;
    public PatrollAction (FSMState owner): base(owner) { }
    public void Init(float speed, float distance, Vector2 startPoint, Transform enemyTransform)
    {
        this.speed = speed;
        this.distance = distance;
        this.startPoint = startPoint;
        this.enemyTransform = enemyTransform;
    }
    public override void OnEnter()
    {
        direction = 1;
        limitMin = startPoint.x - distance*0.5f;
        limitMax = startPoint.x + distance*0.5f;
    }
    public override void OnFixedUpdate()
    {
        Patroll();
    }
    public override void OnExit()
    {

    }
    private void Patroll()
    {
        if(enemyTransform.position.x < limitMin)
        {
            direction = 1f;
        }
        if(enemyTransform.position.x > limitMax)
        {
            direction = -1f;
        }
        enemyTransform.Translate(Vector2.right * direction * speed * Time.fixedDeltaTime);
    }
}
