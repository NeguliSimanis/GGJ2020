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
        pos += velocity;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && rb.velocity.magnitude <= 0)
        {
            other.gameObject.GetComponent<PlayerAttack>().RetrieveHarpoon();
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            other.gameObject.GetComponent<EnemyStats>().DamageEnemy();
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 11)
        {
            Debug.Log("test");
            Destroy(gameObject);
        }
    }


    void StopMovement()
    {
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
    }
}
