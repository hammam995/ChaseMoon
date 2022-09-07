using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFollowerAction : FSMAction
{
    private float speed;
    private Transform enemy;
    private Transform target;
    private Vector2 direction;
    public AttackFollowerAction (FSMState owner): base(owner) { }
    public void Init(float speed, Transform enemy, Transform target)
    {
        this.speed = speed;
        this.enemy = enemy;
        this.target = target;
    }
    public override void OnEnter()
    {
        direction = (target.position - enemy.position).normalized;
        enemy.GetComponent<EnemyFollowers>().StartDeathCoroutine();
        Debug.DrawRay(enemy.position,direction * 5f,Color.green,5f);
    }
    public override void OnFixedUpdate()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        enemy.Translate(direction * speed * Time.fixedDeltaTime);
    }
}
