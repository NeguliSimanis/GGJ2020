using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int Damage;
    public int Delay;

    public bool canAttack;

    public void Attack(GameObject target)
    {
        if (canAttack)
        {
            Debug.Log("Dealt " + Damage + " attack");
            canAttack = false;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown(int Delay)
    {
        yield return new WaitForSeconds(Delay);
        canAttack = true;
    }

}
