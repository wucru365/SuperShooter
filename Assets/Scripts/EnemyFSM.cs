using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public Sight sightSensor;
    public enum EnemyState { ChasePlayer, AttackPlayer}
    public EnemyState currentState;
    public Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;
    void Start()
    {
        
    }

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }
    //void GoToBase() { 
    //    if (sightSensor.detectedObject != null)
    //    {
    //        currentState = EnemyState.ChasePlayer;
    //    }
    //    float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
    //    if (distanceToBase < baseAttackDistance)
    //    {
    //        currentState = EnemyState.AttackBase;
    //    }
    //}
    //void AttackBase() { print("AttackBase"); }
    void ChasePlayer() {
        agent.isStopped = false;
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.AttackPlayer;
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
        print("ChasePlayer");  
    }
    void AttackPlayer() {
        agent.isStopped = true;
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.ChasePlayer;
            return;
        }
        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
        print("AttackPlayer"); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
