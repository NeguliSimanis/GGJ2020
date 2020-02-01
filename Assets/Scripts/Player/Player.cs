﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    public float speed;
    public float Upspeed;
    public bool canMove;

    public bool faceLeft;

    public float vertical;
    public float horizontal;


    public bool hasItem;

    public GameObject GameManager;

    public GameObject pauseUI;

    public PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        speed = 20f;
        Upspeed = 40f;
        canMove = true;

        pauseUI = GameObject.Find("PauseCanvas");
        playerAttack = GetComponent<PlayerAttack>();
        faceLeft = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            pauseUI.GetComponent<PauseScript>().onPause();
        }
    }

    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal < 0)
        {
            faceLeft = true;
        } else
        {
            faceLeft = false;
        } 
        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);

        if(canMove==true)
        {
            rb.AddForce(movement * speed);
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(0f, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Harpoon time");
            playerAttack.Attack();
        }
           
    }

}
