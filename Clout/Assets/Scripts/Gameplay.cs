using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System;

public class Gameplay : MonoBehaviour
{ 
    public TMP_Text followerCountText;
    public Canvas FPSCanvas;
    public Canvas PhoneCanvas;
    public GameObject FPSController;
    public GameObject activePerson;
    public Person person;
    public bool usingPhone;
    public Slider activePersonSlider;
    public TMP_Text activeUserProfileText;
    public ObjectClicker objClick;
    public List<Sprite> imagesList;
    public string[] textPostArray;
    public string[] captionArray;
    public string[] usernamesArray;
    public TextAsset textPostsFile;
    public TextAsset captionsFile;
    public TextAsset usernamesFile;
    public List<string> usernamesList;
    public GameObject personPrefab;
    public int numPeople;
    public List<GameObject> people;
    public AudioClip cloutedUp;
    public AudioClip cloutHalf;
    public AudioClip lonely;
    public AudioSource BGM;
    public AudioSource notificationSound;
    public TMP_Text winningText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PauseHUD());
        people = new List<GameObject>();
        SpawnPeople();
        GenerateTextPostList();
        GenerateCaptionsList();
        GenerateUsernamesList();
        usernamesList = new List<string>(usernamesArray);
    }

    private void GenerateUsernamesList()
    {
        char[] separator = { ',' };
        usernamesArray = usernamesFile.text.Split(separator);
    }

    // Update is called once per frame
    void Update()
    {
        FollowerCountTextUpdate();
        if (activePerson)
        {
            person = activePerson.GetComponent<Person>();

            activePersonSlider.value = person.currentLikePercentage;
            activeUserProfileText.text = person.username + "'s Profile";
        }
        float currentPercentage = (FollowerCount() / numPeople);
        if (currentPercentage <= 0.5f)
        {
            BGM.clip = lonely;
        }
        else if (currentPercentage > 0.5f && currentPercentage < 1f)
        {
            BGM.clip = cloutHalf;
        }
        else
        {
            BGM.clip = cloutedUp;
            foreach(GameObject person1 in people)
            {
                person1.GetComponent<Person>().likePointsDecreaseRate = 0;
            }
            winningText.gameObject.SetActive(true);
        }
    }

    void FollowerCountTextUpdate()
    {
        followerCountText.SetText("Bodies Sacrificed: " + FollowerCount() + " / " + numPeople);
    }

    public void SwitchCanvases()
    {
        usingPhone = !usingPhone;
        FPSCanvas.gameObject.SetActive(!FPSCanvas.gameObject.activeSelf);
        PhoneCanvas.gameObject.SetActive(!PhoneCanvas.gameObject.activeSelf);
        FPSController.gameObject.GetComponent<FirstPersonController>().enabled =
            !FPSController.gameObject.GetComponent<FirstPersonController>().enabled;
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    void GenerateTextPostList()
    {
        char[] separator = { '&' };
        textPostArray = textPostsFile.text.Split(separator);
    }

    void GenerateCaptionsList()
    {
        char[] separator = { '&' };
        captionArray = captionsFile.text.Split(separator);
        foreach(string caption in captionArray)
        {
            if (caption == "empty")
            {
                caption.Replace("empty", "");
            }
        }
    }

    public string PullRandomUsername()
    {
        if (usernamesList.Count > 0)
        {
            usernamesList = ShuffleList(usernamesList);
            string result = usernamesList[usernamesList.Count - 1];
            usernamesList.Remove(result);
            return result;
        }
        else
        {
            return null;
        }
    }

    List<string> ShuffleList(List<string> alpha)
    {
        for (int i = 0; i < alpha.Count; i++)
        {
            string temp = alpha[i];
            int randomIndex = UnityEngine.Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
        return alpha;
    }

    void SpawnPeople()
    {
        for (int i = 0; i < numPeople; i++)
        {
            people.Add(Instantiate(personPrefab, new Vector3(UnityEngine.Random.Range(-5f, 5f),
                0, UnityEngine.Random.Range(-5f, 5f)), Quaternion.identity));
        }
    }

    int FollowerCount()
    {
        int totalFollowers = 0;
        foreach(GameObject person1 in people)
        {
            if(person1.GetComponent<Person>().follower)
            {
                totalFollowers++;
            }
        }
        return totalFollowers;
    }

    IEnumerator PauseHUD()
    {
        yield return new WaitForSeconds(3.4f);
        FPSCanvas.gameObject.SetActive(true);
    }

}

