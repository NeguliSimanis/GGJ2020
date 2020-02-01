using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public float radius = 20;

    public enum EnemyState { WANDER, CHASE};
    public EnemyState ActiveState = EnemyState.WANDER;

    private GameObject playerRef;

    IAstarAI ai;

    void Start () 
    {
        ai = GetComponent<IAstarAI>();
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
                    Debug.Log("Entered wander state");
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
                    Debug.Log("Entered chase state");
                }
                break;

            default:
                {
                    Debug.Log("Entered wander state");
                }
                break;
        }
    }
}
