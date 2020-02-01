using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public float radius = 20;

    public enum EnemyState { WANDER, CHASE, ATTACK};
    public EnemyState ActiveState = EnemyState.WANDER;

    private GameObject playerRef;
    public EnemyAttack attack;

    IAstarAI ai;

    void Start () 
    {
        ai = GetComponent<IAstarAI>();
        attack = GetComponent<EnemyAttack>();
    }

    Vector3 PickRandomPoint () 
    {
        var point = Random.insideUnitSphere * radius;

        point += ai.position;
        Debug.Log(point);
        return point;
    }

    void GetPlayer()
    {
        playerRef = FindObjectOfType<Player>().gameObject;
    }

    float PlayerDistance()
    {
        return Vector2.Distance(playerRef.transform.position, transform.position);
    }

    void Update ()
    {
        if (!playerRef)
        {
            GetPlayer();
        }

        switch (ActiveState)
        {
            case EnemyState.WANDER:
                {
                    //Debug.Log("Entered wander state");
                    ai.maxSpeed = 2;
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
                    ai.maxSpeed = 3;
                    if(PlayerDistance() > 10)
                    {
                        ActiveState = EnemyState.WANDER;
                    }
                    if(PlayerDistance() < 1)
                    {
                        attack.Attack(playerRef);
                    }
                    //Debug.Log("Entered chase state");
                }
                break;

            case EnemyState.ATTACK:
                {
                    if (attack.canAttack)
                    {
                        attack.Attack(playerRef);
                    }
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
