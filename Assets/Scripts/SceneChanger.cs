using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    Animator fadeAnimator;
    float fadeInAnimationLength = 0.2f;
    float fadeOutAnimationLength = 0.9f;

    public void LoadLevel(string levelName)
    {

    }

}
