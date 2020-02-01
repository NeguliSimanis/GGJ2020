using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnPOS;

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
            Instantiate(projectile, spawnPOS.transform.position, Quaternion.identity);
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
