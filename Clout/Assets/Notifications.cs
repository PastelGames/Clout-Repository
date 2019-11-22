using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notifications : MonoBehaviour
{
    public TMP_Text notificationText;
    public Gameplay gameplay;
    HashSet<string> notifiedPeople;

    // Start is called before the first frame update
    void Start()
    {
        notifiedPeople = new HashSet<string>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject person in gameplay.people)
        {
            Person personC = person.GetComponent<Person>();
            if (personC.follower && personC.currentLikePercentage < .2f)
            {
                if (notifiedPeople.Contains(personC.username) == false)
                {
                    gameplay.notificationSound.Play();
                    GiveUserFeedback(person.GetComponent<Person>().username + " is about to unfollow you!", Color.yellow, 5f);
                    Debug.Log(notifiedPeople.Contains(personC.username));
                    notifiedPeople.Add(personC.username);
                    Debug.Log(notifiedPeople.Count);
                }
            }
            else
            {
                notifiedPeople.Remove(personC.username);
            }
        }
    }

    public void GiveUserFeedback(string userFeedbackString, Color feedbackColor, float timeToDisplayFeedback)
    {
        TMP_Text newUserFeedbackText =
            Instantiate<TMP_Text>(notificationText, this.transform);
        newUserFeedbackText.text = userFeedbackString;
        newUserFeedbackText.color = feedbackColor;
        newUserFeedbackText.fontSize = 15;
        newUserFeedbackText.alignment = TextAlignmentOptions.MidlineLeft;
        Destroy(newUserFeedbackText.gameObject, timeToDisplayFeedback);
    }
}
