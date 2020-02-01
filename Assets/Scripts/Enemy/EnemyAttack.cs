using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int Delay;

    public bool canAttack;

    public void Start()
    {
        canAttack = true;
    }

    public void Attack(GameObject target)
    {
        if (canAttack)
        {
            Debug.Log("attack");
            target.GetComponent<PlayerHealth>().DamagePlayer();
            canAttack = false;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Delay);
        canAttack = true;
    }

}
