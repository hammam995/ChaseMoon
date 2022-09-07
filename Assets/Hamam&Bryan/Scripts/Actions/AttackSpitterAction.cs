using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpitterAction : FSMAction
{
    private float timeSpawn;
    private string finishEvent;
    private EnemySpitters enemy;

    private float time;
    public AttackSpitterAction(FSMState owner): base(owner) { }
    public void Init(float timeSpawn, EnemySpitters enemy, string finishEvent)
    {
        this.timeSpawn = timeSpawn;
        this.enemy = enemy;
        this.finishEvent = finishEvent;
    }
    public override void OnEnter()
    {
        Debug.Log("ATTACK STATE");
        time = 0;
        enemy.SpawnBullet();
    }
    public override void OnFixedUpdate()
    {
        if (!StillDetectPlayer())
        {
            FinishEvent(finishEvent);
            return;
        }
        if (TimeFinished(timeSpawn))
            enemy.SpawnBullet();
    }
    private bool TimeFinished(float timeSpawn)
    {
        time += Time.fixedDeltaTime;
        if(time > timeSpawn)
        {
            time = 0;
            return true;
        }
        return false;
    }
    private bool StillDetectPlayer()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.GetTarget().position) < enemy.GetRadius())
            return true;
        else
            return false;
    }
    private void FinishEvent(string finishEvent)
    {
        Debug.Log("DETECCION STATE");
        if (finishEvent != null)
            GetOwner().SendEvent(finishEvent);
    }
}
