using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{
    bool sceneLoadCalled = false;
    float loadNextSceneAfter = 3f;
    void Start()
    {
       // StartCoroutine(LoadGameplaySceneAfterDelay());
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
        yield return new WaitForSeconds(loadNextSceneAfter);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (sceneLoadCalled)
            return;
        sceneLoadCalled = true;
        Debug.Log("NEXT SCENE LOAD CALLED");
        SceneChanger.instance.LoadLevelAfterFade("3_GodScreen");
    }
}
