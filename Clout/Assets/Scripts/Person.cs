using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
    public bool follower;
    GameObject usernameCanvas;
    public int popularityMin;
    public int popularityDegree;
    public float likePoints;
    public float maxLikePoints;
    public string username;
    public TMP_Text usernameText;
    public Slider slider;
    public float likePointsDecreaseRate;
    public float currentLikePercentage;
    public List<GameObject> posts;
    public GameObject textPost;
    public GameObject photoPost;
    public Sprite dummyImage;
    public int postsLength = 30;
    Material nonFollowerMaterial;
    public Material followerMaterial;
    public bool postsInstantiated;
    public PhoneContent phoneContent;
    Gameplay gameplay;
    public Notifications notifications;
    public GameObject hair;
    public GameObject torso;
    public Material source;
    //public List<Mesh> hairMeshes;
    Material nonFollowerHairMaterial;

    // Start is called before the first frame update
    void Start()
    {
        usernameCanvas = transform.Find("Username Canvas").gameObject;
        popularityMin = Random.Range(0, 10000);
        popularityDegree = Random.Range(0, 5000);
        slider.maxValue = maxLikePoints;
        GenerateRandomPosts(postsLength);
        nonFollowerMaterial = new Material(source);
        followerMaterial = source;
        Color nonFollowerColor = UnityEngine.Random.ColorHSV();
        Color hairColor = UnityEngine.Random.ColorHSV();
        Debug.Log(hairColor.ToString());
        Debug.Log(nonFollowerColor.ToString());
        nonFollowerHairMaterial = new Material(source);
        nonFollowerHairMaterial.color = hairColor;
        nonFollowerColor.a = .5f;
        nonFollowerMaterial.color = nonFollowerColor;
        //hair.GetComponent<MeshFilter>().mesh = ChooseRandomMesh(hairMeshes);
        //hair.GetComponent<MeshCollider>().sharedMesh = ChooseRando
    }

    // Update is called once per frame
    void Update()
    {
        if (follower)
        {
            hair.GetComponent<MeshRenderer>().material = followerMaterial;
            torso.GetComponent<SkinnedMeshRenderer>().material = followerMaterial;
            if (!usernameCanvas.activeSelf) usernameCanvas.SetActive(true);
            slider.value = likePoints;
            likePoints -= Time.deltaTime * likePointsDecreaseRate;
            if (likePoints <= 0)
            {
                follower = false;
                usernameCanvas.SetActive(false);
                gameplay.notificationSound.Play();
                notifications = GameObject.Find("Notifications").GetComponent<Notifications>();
                notifications.GiveUserFeedback(username + " just unfollowed you.", Color.red, 5f);
            }
        }
        else
        {
            torso.GetComponent<SkinnedMeshRenderer>().material = nonFollowerMaterial;
            hair.GetComponent<MeshRenderer>().material = nonFollowerHairMaterial;
        }
        likePoints = Mathf.Clamp(likePoints, 0, maxLikePoints);
        currentLikePercentage = likePoints / maxLikePoints;
    }

    //Mesh ChooseRandomMesh(List<Mesh> mesh)
    //{
    //    //return hairMeshes[Random.Range(0, hairMeshes.Count - 1)];
    //}

    void GenerateRandomPosts(int numPosts)
    {
        gameplay = GameObject.FindGameObjectWithTag("Gameplay").GetComponent<Gameplay>();
        username = "@" + gameplay.PullRandomUsername();
        usernameText.text = username;
        int numTextPosts;
        int numPhotoPosts;
        numTextPosts = Random.Range(0, numPosts);
        numPhotoPosts = numPosts - numTextPosts;
        for (int i = 0; i < numTextPosts; i++)
        {
            GameObject newPost = Instantiate(textPost);
            newPost.GetComponent<Post>().usernameText = username;
            newPost.GetComponent<Post>().numLikes = Random.Range(popularityMin,
                popularityMin + popularityDegree);
            newPost.GetComponent<Post>().postText =
                gameplay.textPostArray[Random.Range(0, gameplay.textPostArray.Length)];
            posts.Add(newPost);
            newPost.SetActive(false);
        }
        for (int i = 0; i < numPhotoPosts; i++)
        {
            int random = Random.Range(0, gameplay.imagesList.Count - 1);
            GameObject newPost = Instantiate(photoPost);
            newPost.GetComponent<Post>().usernameText = username;
            newPost.GetComponent<Post>().numLikes = Random.Range(popularityMin,
                popularityMin + popularityDegree);
            newPost.GetComponent<Post>().postText = gameplay.captionArray[random];
            newPost.GetComponent<Post>().postSprite = gameplay.imagesList[random];
            posts.Add(newPost);
            newPost.SetActive(false);
        }
    }

    public void FirstInteraction()
    {
        postsInstantiated = true;
    }

}
