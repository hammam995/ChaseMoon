using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFSM : MonoBehaviour, IKillable
{
    [Header("Enemy Setting")]
    [SerializeField] protected bool upOrDown;
    [Header("Enemy Detection")]
    [SerializeField] protected float radius;

    protected Transform targetPlayer;
    protected enum TypeEnemy { Followers, Spitters }
    protected TypeEnemy type;
    protected FSM enemyFSM;
    protected FSMState IdleState;
    protected FSMState AttackState;
    protected DetectionAction Detection;
    public FSMState GetCurrentState()
    {
        return enemyFSM.GetCurrentState();
    }
    protected virtual void Awake()
    {
        foreach(MainCharacterFSM mc in FindObjectsOfType<MainCharacterFSM>())
        {
            targetPlayer = mc.GetCharacterUpOrDown() == upOrDown ? mc.transform : targetPlayer;
        }
    }
    protected virtual void Start()
    {
        enemyFSM = new FSM("Enemies");

        IdleState = enemyFSM.AddState("Idle");
        AttackState = enemyFSM.AddState("Attack");

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable player = collision.GetComponent<IKillable>();
        if(player != null)
        {
            player.Kill();
        }
        // Destroy this object
    }
    public void Kill()
    {
        // Animations
        Destroy(this.gameObject);
    }
#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
