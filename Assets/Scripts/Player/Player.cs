using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    public float speed;
    public float Upspeed;
    public bool canMove;


    public bool hasItem;

    public GameObject GameManager;

    public GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        speed = 20f;
        Upspeed = 40f;
        canMove = true;

        pauseUI = GameObject.Find("PauseCanvas");

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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
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
        
           
    }

}
