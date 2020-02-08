using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives;
    public int currentLives;
    public int Damage;

    public List<Image> hearts = new List<Image>();

    public GameObject DeathScreen;
    [SerializeField]
    private AudioClip PlayerHurtAudio;

    //TEMP VARIABLES
    public bool toDamage;
    public bool toHeal;

    private Player playerMovement;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        maxLives = 5;
        currentLives = maxLives;

        for(int i = 1; i <= 5; i++)
        {
            Image heart = GameObject.Find("Heart" + i).GetComponent<Image>();
            hearts.Add(heart);
        }

        toDamage = false;
        toHeal = false;

        DeathScreen = GameObject.Find("GameOverPanel");
        playerMovement = this.GetComponent<Player>();

       // PlayerHurtAudio = GameObject.Find("SoundObject-PlayerHurt");
    }

    // Update is called once per frame
    void Update()
    {
        if(toDamage == true)
        {
            DamagePlayer();
            toDamage = false;
        }
        if (toHeal == true)
        {
            HealPlayer();
            toHeal = false;
        }
    }

    public void DamagePlayer()
    {
        currentLives = currentLives - 1;
        reduceHearts();

        if (currentLives <= 0)
        {
            currentLives = 0;
            anim.speed = 0;
            StartCoroutine(DeathSequence());
        }
        else
            gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerHurtAudio);

    }

    public void HealPlayer()
    {
        currentLives++;
        if (currentLives > maxLives)
            currentLives = maxLives;

    }

    private void reduceHearts()
    {
        hearts[currentLives].enabled = false;
    }

    IEnumerator DeathSequence()
    {
        //DeathScreen.GetComponent<Animator>().enabled = true;
        GetComponent<AudioSource>().Play();
        playerMovement.canMove = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject, 2f);
        GameObject.FindGameObjectWithTag("God").GetComponent<GodController>().DealWithPlayerDeath();    
    }
}
