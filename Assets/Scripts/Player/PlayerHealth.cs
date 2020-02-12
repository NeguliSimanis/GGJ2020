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

    private Image DamageImage;

    public List<Image> hearts = new List<Image>();

    public GameObject DeathScreen;
    [SerializeField]
    private AudioClip PlayerHurtAudio;

    //TEMP VARIABLES
    public bool toDamage;
    public bool toHeal;

    private bool flashing;
    public bool isInvulnerable;

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

        DamageImage = GameObject.Find("DamageImage").GetComponent<Image>();

        toDamage = false;
        toHeal = false;
        flashing = false;

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
        if (!isInvulnerable)
        {
            currentLives = currentLives - 1;
            reduceHearts();
            StartCoroutine("Flash");

            if (currentLives <= 0)
            {
                currentLives = 0;
                anim.speed = 0;
                StartCoroutine(DeathSequence());
            }
            else
            {
                StartCoroutine("Invulnerable");
                gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerHurtAudio);
            }

        }

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
        if(currentLives != 0)
        {
            float modifier = 10 / currentLives;
            DamageImage.SetTransparency(0.05f * modifier);
        }
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

    public IEnumerator Flash()
    {
        flashing = true;
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0.5f);
        yield
        return new WaitForSeconds(0.25f);
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0);
        yield
        return new WaitForSeconds(0.1f);
        flashing = false;
    }

    private IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        for (int n = 0; n < 4; n++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.15f);
        }
        isInvulnerable = false;
    }

}
