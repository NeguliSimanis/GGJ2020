using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public int enemySpeed;
    public int enemyChaseSpeed;

    public int enemyHealth;

    public SpriteRenderer enemySprite;

    public Sprite dedSprite;
    public Animator anim;

    public EnemyMovement movement;

    public void Start()
    {
        enemyHealth = 100;
        movement = GetComponent<EnemyMovement>();
    }

    public void DamageEnemy()
    {
        enemyHealth =- 50;
        Debug.Log("Enemy health at: "+enemyHealth);
        if(enemyHealth <= 0)
        {
            GetComponent<AudioSource>().Play();
            enemySprite.GetComponent<SpriteRenderer>().sprite = dedSprite;
            anim.SetBool("Dead", true);
            StartCoroutine("Die");
            movement.ai.canMove = false;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<LootInventory>().SpawnItem();
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
