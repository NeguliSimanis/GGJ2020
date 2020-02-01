using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{

    float loadNextSceneAfter = 3f;
    void Start()
    {
        StartCoroutine(LoadGameplaySceneAfterDelay());
    }

   
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("3_GodScreen");
        }
    }

    private IEnumerator LoadGameplaySceneAfterDelay()
    {
        Debug.Log("stat");
        yield return new WaitForSeconds(loadNextSceneAfter);
        SceneManager.LoadScene("3_GodScreen");
    }
}
