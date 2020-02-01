using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitPointScript : MonoBehaviour
{

    public GameObject text;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject GO = collision.gameObject;
            if (GO.GetComponent<Player>().hasItem == true)
                isActive = true;
            else
                isActive = false;

            if(isActive==true)
            {
                StartCoroutine(ExitSequence(GO));
            }

        }
    }

    IEnumerator ExitSequence(GameObject GO)
    {

        Debug.Log("PLAYER exited");
        Destroy(GO.gameObject);
        text.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");


    }
}
