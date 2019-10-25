using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public bool follower;
    GameObject usernameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        usernameCanvas = gameObject.transform.Find("Username Canvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if (follower == true)
        {
            usernameCanvas.SetActive(true);
        }
    }
}
