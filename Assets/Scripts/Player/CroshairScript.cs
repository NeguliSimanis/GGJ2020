using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroshairScript : MonoBehaviour
{
    private Vector3 mouseCoordinates;
    public float mouseSensitivity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mouseCoordinates.x, mouseCoordinates.y, -1f);

    }
}
