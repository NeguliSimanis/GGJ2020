using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private Rigidbody2D rb;

    Player player;
    Vector3 pos;
    Vector3 velocity;

    private AudioSource ac;
    public AudioClip metalImpact;

    bool impact = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<AudioSource>();

        player = FindObjectOfType<Player>(); //Get a direction vector to fire the bullet at.
        velocity = new Vector3(10 * Time.deltaTime, 0, 0);
        pos = transform.position;

        if (player.transform.rotation.y == 0)
        {
            velocity = new Vector3(10 * 1 * Time.deltaTime, 0, 0);
        }

        else
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
            velocity = new Vector3(10 * -1 * Time.deltaTime, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!impact)
        {
            pos += velocity;
            transform.position = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            other.gameObject.GetComponent<EnemyStats>().DamageEnemy();
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 11)
        {
            impact = true;
            ac.PlayOneShot(metalImpact);
            StartCoroutine("DestroyObject");
        }
    }


    void StopMovement()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
    
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
