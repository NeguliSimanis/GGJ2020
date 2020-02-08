using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [HideInInspector]
    public EnemySpawningScript enemySpawningScript;
    public ItemType enemyType;
    public int enemySpeed;
    public int enemyChaseSpeed;

    public int enemyHealth;
    private bool flashing;

    public SpriteRenderer enemySprite;

    public Sprite dedSprite;
    public SpriteRenderer currentSprite;
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
        StartCoroutine("Flash");
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
        movement.canAttack = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<LootInventory>().SpawnItem();
        //gameObject.SetActive(false);
        
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public IEnumerator Flash()
    {
        flashing = true;
        currentSprite.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.8f);
        yield
        return new WaitForSeconds(0.25f);
        currentSprite.GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0);
        yield
        return new WaitForSeconds(0.1f);
        flashing = false;
    }

    private void OnDestroy()
    {
        if (enemySpawningScript == null)
            return;
            switch (enemyType)
        {
            case ItemType.Boot:
                enemySpawningScript.bootFishes.Remove(this.gameObject);
                break;
            case ItemType.Skull:
                enemySpawningScript.skullFishes.Remove(this.gameObject);
                break;
            case ItemType.Worm:
                enemySpawningScript.wormFishes.Remove(this.gameObject);
                break;
        }
    }

}
