using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowers : EnemyFSM
{
    [Header("Patroll Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [Header("Attack Settings")]
    [SerializeField] private float speedAttack;
    [Header("Death Settings")]
    [SerializeField] private float maxTimeAttacking;

    private FSMState PatrollState;
    private PatrollAction Patroll;
    private AttackFollowerAction Attack;

    private Vector3 IdlePos;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        PatrollState = enemyFSM.AddState("Patroll");

        Detection = new DetectionAction(PatrollState);
        Patroll = new PatrollAction(PatrollState);
        Attack = new AttackFollowerAction(AttackState);

        PatrollState.AddAction(Detection);
        PatrollState.AddAction(Patroll);
        AttackState.AddAction(Attack);

        IdleState.AddTransition("ToPatroll", PatrollState);
        PatrollState.AddTransition("ToIdle", IdleState);
        PatrollState.AddTransition("ToAttack", AttackState);

        Detection.Init(radius, upOrDown, transform, "ToAttack");
        Patroll.Init(speed, distance, transform.position, transform);
        Attack.Init(speedAttack, transform, targetPlayer);

        enemyFSM.Start("Patroll");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        enemyFSM.FixedUpdate();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
    }
    public void StartDeathCoroutine()
    {
        StartCoroutine(DeathAttacking());
    }
    private IEnumerator DeathAttacking()
    {
        yield return new WaitForSeconds(maxTimeAttacking);
        Kill();
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        IdlePos = IdlePos == Vector3.zero ? transform.position : IdlePos;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(IdlePos, new Vector3(distance, 1, 1));
    }
#endif
}
