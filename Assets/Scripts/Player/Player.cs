using UnityEngine;

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

    public SpriteRenderer sprite;

    public float maxVelocity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        speed = 10f;
        Upspeed = 10f;
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
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        if(horizontal > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        Vector3 movement = new Vector3(horizontal, vertical, 0.0f);


        if(canMove==true)
        {
            rb.AddRelativeForce(movement * speed);

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(0f, movement.x) * Mathf.Rad2Deg;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Harpoon time");
            playerAttack.Attack();
        }

    }

}
