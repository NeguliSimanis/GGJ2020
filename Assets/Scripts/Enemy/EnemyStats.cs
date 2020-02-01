using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int enemySpeed;
    public int enemyChaseSpeed;

    public int enemyHealth;

    public EnemyMovement movement;

    public void Start()
    {
        enemyHealth = 100;
        movement = GetComponent<EnemyMovement>();
    }

    public void DamageEnemy()
    {
        enemyHealth =- 50;
        if(enemyHealth <= 0)
        {
            StartCoroutine("Die");
            movement.ai.canMove = false;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
