using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitters : EnemyFSM
{
    [Header("Attack Settings")]
    [SerializeField] private float timeSpawn;
    [Header("Bullet Object")]
    [SerializeField] private GameObject bulletPrefab;
    [Header("Spawn Transform")]
    [SerializeField] private Transform spawnPoint;

    private FSMState DetectionState;
    private AttackSpitterAction Attack;

    public Transform GetTarget()
    {
        return targetPlayer;
    }
    public float GetRadius()
    {
        return radius;
    }
    public bool GetUpOrDown()
    {
        return upOrDown;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DetectionState = enemyFSM.AddState("Detection");

        Detection = new DetectionAction(DetectionState);
        Attack = new AttackSpitterAction(AttackState);

        DetectionState.AddAction(Detection);
        AttackState.AddAction(Attack);

        IdleState.AddTransition("ToDetection", DetectionState);
        DetectionState.AddTransition("ToIdle", IdleState);
        DetectionState.AddTransition("ToAttack", AttackState);
        AttackState.AddTransition("ToDetection", DetectionState);
        AttackState.AddTransition("ToIdle", IdleState);

        Detection.Init(radius, upOrDown, transform, "ToAttack");
        Attack.Init(timeSpawn, this, "ToDetection");

        enemyFSM.Start("Detection");
    }
    public void SpawnBullet()
    {
        GameObject Bullet = Instantiate(bulletPrefab, spawnPoint.position, bulletPrefab.transform.rotation);
        BulletSpitters bulletScript = Bullet.GetComponent<BulletSpitters>();
        bulletScript.SetSpawnPoint(spawnPoint.position);
        bulletScript.SetTarget(targetPlayer);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        enemyFSM.FixedUpdate();
    }
}
