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
            LoadNextScene();
            
        }
    }

    private IEnumerator LoadGameplaySceneAfterDelay()
    {
        Debug.Log("stat");
        yield return new WaitForSeconds(loadNextSceneAfter);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneChanger.instance.LoadLevelAfterFade("3_GodScreen");
    }
}
