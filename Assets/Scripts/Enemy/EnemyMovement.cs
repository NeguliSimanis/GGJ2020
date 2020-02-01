using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public float radius = 20;

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

    void Update () {
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }
}
