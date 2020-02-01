using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives;
    public int currentLives;
    public int Damage;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public bool toDamage;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(toDamage == true)
        {
            DamagePlayer();
            toDamage = false;
        }
    }

    public void DamagePlayer()
    {
        currentLives = currentLives - 1;

        if (currentLives == 2)
            heart1.enabled = false;
        else if (currentLives == 1)
            heart2.enabled = false;
        else if (currentLives == 0)
            heart3.enabled = false;

        if (currentLives < 0)
        {

        }

    }
}
