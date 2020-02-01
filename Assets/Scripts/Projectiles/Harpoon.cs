using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private Rigidbody2D rb;

    Player player;
    Vector3 pos;
    Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<Player>(); //Get a direction vector to fire the bullet at.
        velocity = new Vector3(10 * Time.deltaTime, 0, 0);
        pos = transform.position;

        if (!player.GetComponent<Player>().faceLeft)
        {
            velocity = new Vector3(10 * 1 * Time.deltaTime, 0, 0);
        }

        else
        {
            velocity = new Vector3(10 * -1 * Time.deltaTime, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        pos += velocity;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && rb.velocity.magnitude <= 0)
        {
            other.GetComponent<PlayerAttack>().RetrieveHarpoon();
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.layer == 11)
        {
            StopMovement();
        }
       if(other.gameObject.tag == "Enemy")
        {
            StopMovement();
            other.gameObject.GetComponent<EnemyStats>().DamageEnemy();
        }
    }

    void StopMovement()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
}
