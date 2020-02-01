using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    public float speed;
    public float Upspeed;

    public int maxHP;
    public int currentHP;
    public int Damage;

    public bool hasItem;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        speed = 20f;
        Upspeed = 40f;
        maxHP = 100;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);

        rb.AddForce(movement * speed);

        if(movement != Vector3.zero)
        {
            float angle = Mathf.Atan2(0f, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void DamagePlayer(int damage)
    {
        currentHP = currentHP - damage;
    }

}
