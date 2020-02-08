using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float radius = 20;

    public enum EnemyState { WANDER, CHASE, ATTACK};
    public EnemyState ActiveState = EnemyState.WANDER;

    private GameObject playerRef;

    private EnemyAttack attack;
    private EnemyStats stats;

    public IAstarAI ai;

    public bool canAttack;

    void Start () 
    {
        ai = GetComponent<IAstarAI>();
        attack = GetComponent<EnemyAttack>();
        stats = GetComponent<EnemyStats>();
        canAttack = true;
    }

    Vector3 PickRandomPoint () 
    {
        var point = Random.insideUnitSphere * radius;

        point += ai.position;
        return point;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");
    }

    void GetPlayer()
    {
        playerRef = FindObjectOfType<Player>().gameObject;
    }

    private IEnumerator Cooldown()
    {
        Debug.Log("starting cooldown");
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    float PlayerDistance()
    {
        return Vector2.Distance(playerRef.transform.position, transform.position);
    }

    void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, 0, 0));

        if (!playerRef)
        {
            GetPlayer();
        }

        switch (ActiveState)
        {
            case EnemyState.WANDER:
                {
                    //Debug.Log("Entered wander state");
                    ai.maxSpeed = stats.enemySpeed;
                    if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
                    {
                        ai.destination = PickRandomPoint();
                        ai.SearchPath();
                    }
                    if (PlayerDistance() < 4)
                    {
                        ActiveState = EnemyState.CHASE;
                    }
                }
                break;

            case EnemyState.CHASE:
                {
                    ai.destination = playerRef.transform.position;
                    ai.maxSpeed = stats.enemyChaseSpeed;
                    if(PlayerDistance() > 10)
                    {
                        ActiveState = EnemyState.WANDER;
                    }
                    if(PlayerDistance() < 1.5f)
                    {
                        if (canAttack)
                        {
                            canAttack = false;
                            StartCoroutine("Cooldown");
                            playerRef.GetComponent<PlayerHealth>().DamagePlayer();
                        }
                    }
                    //Debug.Log("Entered chase state");
                }
                break;

            case EnemyState.ATTACK:
                {
                    attack.Attack(playerRef);
                    ActiveState = EnemyState.CHASE;
                }
                break;

            default:
                {
                    //Debug.Log("Entered wander state");
                }
                break;
        }
    }
}
