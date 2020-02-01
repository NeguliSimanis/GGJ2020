using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{

    public GameObject healthUI;
    // Start is called before the first frame update
    void Start()
    {
        healthUI = GameObject.Find("HealthPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPause()
    {
        healthUI.SetActive(false);
        this.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0.0f;
    }

    public void onResume()
    {
        healthUI.SetActive(true);
        Time.timeScale = 1.0f;
        this.GetComponent<Canvas>().enabled = false;
    }

    public void onMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
