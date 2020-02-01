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

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public GameObject DeathScreen;
    public GameObject PlayerHurtAudio;

    //TEMP VARIABLES
    public bool toDamage;
    public bool toHeal;

    private Player playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        maxLives = 3;
        currentLives = maxLives;

        heart1 = GameObject.Find("Heart1").GetComponent<Image>();
        heart2 = GameObject.Find("Heart2").GetComponent<Image>();
        heart3 = GameObject.Find("Heart3").GetComponent<Image>();

        heart1.enabled = true;
        heart2.enabled = true;
        heart3.enabled = true;

        toDamage = false;
        toHeal = false;

        DeathScreen = GameObject.Find("GameOverPanel");
        playerMovement = this.GetComponent<Player>();

        PlayerHurtAudio = GameObject.Find("SoundObject-PlayerHurt");
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

        healthCheck();

        if (currentLives <= 0)
        {
            currentLives = 0;
            StartCoroutine(DeathSequence());      
        }
        else
            PlayerHurtAudio.GetComponent<AudioSource>().Play();

    }

    public void HealPlayer()
    {
        currentLives++;
        if (currentLives > maxLives)
            currentLives = maxLives;

        healthCheck();
    }


    public void healthCheck()
    {
        if (currentLives == 3)
        {
            heart1.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
        }else if( currentLives == 2)
        {
            heart1.enabled = false;
            heart2.enabled = true;
            heart3.enabled = true;
        }else if (currentLives == 1)
        {
            heart1.enabled = false;
            heart2.enabled = false;
            heart3.enabled = true;
        }
        else if (currentLives == 0)
        {
            heart1.enabled = false;
            heart2.enabled = false;
            heart3.enabled = false;
        }
    }

    IEnumerator DeathSequence()
    {
        //DeathScreen.GetComponent<Animator>().enabled = true;
        this.GetComponent<AudioSource>().Play();
        playerMovement.canMove = false;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject, 2f);
        GameObject.FindGameObjectWithTag("God").GetComponent<GodController>().DealWithPlayerDeath();    
    }
}
