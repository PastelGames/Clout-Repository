using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Gameplay : MonoBehaviour
{
    public int followers;
    public TMP_Text followerCountText;
    public Canvas FPSCanvas;
    public Canvas PhoneCanvas;
    public GameObject FPSController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowerCountTextUpdate();
    }

    void FollowerCountTextUpdate()
    {
        followerCountText.SetText("Bodies Sacrificed: " + followers);
    }

    public void SwitchCanvases()
    {
        FPSCanvas.gameObject.SetActive(!FPSCanvas.gameObject.activeSelf);
        PhoneCanvas.gameObject.SetActive(!PhoneCanvas.gameObject.activeSelf);
        FPSController.gameObject.GetComponent<FirstPersonController>().enabled =
            !FPSController.gameObject.GetComponent<FirstPersonController>().enabled;
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

}

