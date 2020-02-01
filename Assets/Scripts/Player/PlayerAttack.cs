using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnPOS;

    public GameObject HarpoonSoundObject;

    public float cooldown;

    private bool canAttack;

    public Animator anim;

    bool hasFired;

    private void Start()
    {
        canAttack = true;
        HarpoonSoundObject = GameObject.Find("SoundObject-HarpoonSound");
    }

    public void Attack()
    {
        if (canAttack)
        {
            //dont look at this code, ill rewrite somethng better when we have actual visuals
            //HarpoonSoundObject.GetComponent<AudioSource>().Play();
            anim.SetBool("Attack", true);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        hasFired = false;
    }

    public void RetrieveHarpoon()
    {
        StopAllCoroutines();
        canAttack = true;
        hasFired = false;
    }

    public void SpawnProjectile()
    {
        if (!hasFired)
        {
            Instantiate(projectile, spawnPOS.transform.position, Quaternion.identity);
            hasFired = true;
        }
        canAttack = false;
        anim.SetBool("Attack", false);
        StartCoroutine("Cooldown");
    }

}
