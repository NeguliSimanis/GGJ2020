using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodText : MonoBehaviour
{
    Text txt;
    string story;

    public bool typing;

    public void GetText(string text)
    {
        StopAllCoroutines();

        txt = GetComponent<Text>();
        story = text;
        txt.text = "";
        typing = false;

        StartCoroutine("PlayText");
    }

    public void StopText()
    {
        txt.text = "";
        StopAllCoroutines();
        typing = false;
    }

    IEnumerator PlayText()
    {
        typing = true;
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }

    public void ShowFullText()
    {
        txt.text = story;
        typing = false;
        StopAllCoroutines();
    }
}
