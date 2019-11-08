using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public TMP_Text numberOfLikesText;
    public TMP_Text postBody;
    public TMP_Text usernameTextBox;
    public Button likeButton;
    public Sprite postSprite;

    public GameObject gameplayObj;

    public string postText;
    public Image postImage;
    public string usernameText;
    public int numLikes;

    public bool liked;
    public bool imageInPost;

    Gameplay gameplay;

    // Start is called before the first frame update
    void Start()
    {
        gameplayObj = GameObject.Find("Gameplay");
        gameplay = gameplayObj.GetComponent<Gameplay>();
        likeButton.onClick.AddListener( delegate { LikePost(); } );
        postBody.text = postText;
        usernameTextBox.text = usernameText;
        if (imageInPost)
        {
            postImage.sprite = postSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        numberOfLikesText.text = numLikes.ToString();
    }

    void LikePost()
    {
        if (!liked)
        {
            liked = true;
        }
        likeButton.interactable = false;
        gameplay.person.likePoints += 20;
        numLikes++;
        gameplay.notificationSound.Play();
    }

    
}
