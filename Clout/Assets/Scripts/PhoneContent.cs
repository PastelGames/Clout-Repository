using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(List<GameObject> posts)
    {
        posts = ShuffleList(posts);
        foreach(GameObject post in posts)
        {
            post.transform.SetParent(transform);
            post.SetActive(true);
        }
    }

    public void SleepCurrentPosts()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Repopulate(List<GameObject> posts)
    {
        SleepCurrentPosts();
        foreach(GameObject post in posts)
        {
            post.SetActive(true);
        }
    }

    List<GameObject> ShuffleList(List<GameObject> alpha)
    {
        for (int i = 0; i < alpha.Count; i++)
        {
            GameObject temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
        return alpha;
    }
}
