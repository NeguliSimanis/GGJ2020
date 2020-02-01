using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public int cooldown;

    private bool canAttack;

    private void Start()
    {
        canAttack = true;
    }

    public void Attack()
    {
        if (canAttack)
        {
            //dont look at this code, ill rewrite somethng better when we have actual visuals
            float direction = GetComponent<Player>().horizontal;
            if(direction == 0)
            {
                direction = 2;
            }
            if(direction < 0)
            {
                direction = -2;
            }
            if(direction == 1)
            {
                direction = 2;
            }
            Vector2 projectileSpawn = new Vector2(transform.position.x + direction, transform.position.y);
            Instantiate(projectile, projectileSpawn, Quaternion.identity);
            canAttack = false;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public void RetrieveHarpoon()
    {
        StopAllCoroutines();
        canAttack = true;
    }

}
