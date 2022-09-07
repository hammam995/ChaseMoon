using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpitters : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float speedFollowers;
    [SerializeField] private float force;
    [SerializeField] private float maxTimeLife;
    [SerializeField] private TypeBullet type;

    private Transform target;
    private Rigidbody2D myRigidbody;
    private Vector2 direction;
    private Vector3 SpawnPoint;

    private float refreshDirection;
    public void SetSpawnPoint(Vector3 SpawnPoint)
    {
        this.SpawnPoint = SpawnPoint;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private enum TypeBullet { Linear, Rigidbody, Follower}
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        refreshDirection = 0;
        Destroy(this.gameObject, maxTimeLife);
        direction = (target.position - SpawnPoint).normalized;
        myRigidbody.gravityScale = target.GetComponent<Rigidbody2D>().gravityScale;
        switch (type)
        {
            case TypeBullet.Linear:
                myRigidbody.bodyType = RigidbodyType2D.Kinematic;
                break;
            case TypeBullet.Rigidbody:
                myRigidbody.bodyType = RigidbodyType2D.Dynamic;
                break;
            case TypeBullet.Follower:
                myRigidbody.bodyType = RigidbodyType2D.Kinematic;
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Behaviour();
    }
    private void Behaviour()
    {
        switch(type)
        {
            case TypeBullet.Linear:
                LinearBullet();
                break;
            case TypeBullet.Rigidbody:
                RigidbodyBullet();
                break;
            case TypeBullet.Follower:
                FollowerBullet();
                break;
        }
    }
    private void LinearBullet()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }
    private void RigidbodyBullet()
    {
        myRigidbody.AddForce(direction * force);
    }
    private void FollowerBullet()
    {
        if (refreshDirection == 15)
        {
            direction = (target.position - transform.position).normalized;
            refreshDirection = 0;
        } else
        {
            refreshDirection++;
        }
        transform.Translate(direction * speedFollowers * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable player = collision.GetComponent<IKillable>();
        if (player != null && !collision.GetComponent<BulletSpitters>())
            player.Kill();
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.layer != 7)
            Destroy(gameObject);
    }
}
