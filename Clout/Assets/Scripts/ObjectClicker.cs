using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Gameplay gameplay;
    public PhoneContent phoneContent;
    public AudioSource notificationSound;
    public Notifications notifications;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameplay.PhoneCanvas.gameObject.activeSelf)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null &
                    hit.transform.gameObject.tag == "Person")
                {
                    gameplay.SwitchCanvases();
                    if (hit.transform.gameObject.GetComponent<Person>().follower == false) {
                        hit.transform.gameObject.GetComponent<Person>().follower = true;
                        notificationSound.Play();
                        notifications.GiveUserFeedback(hit.transform.gameObject.GetComponent<Person>().username
                            + " just followed you!", Color.green, 5f);
                        if (gameplay.activePerson != hit.transform.gameObject)
                        {
                            phoneContent.Repopulate(
                                hit.transform.gameObject.GetComponent<Person>().posts);
                        }
                        gameplay.activePerson = hit.transform.gameObject;
                        gameplay.person = gameplay.activePerson.GetComponent<Person>();
                        gameplay.person.likePoints =
                            gameplay.person.maxLikePoints / 2;
                        if (gameplay.person && !gameplay.person.postsInstantiated)
                        {
                            
                            phoneContent.Populate(gameplay.person.posts);
                            gameplay.person.FirstInteraction();
                        } 
               
                    }
                    else
                    {
                        if (gameplay.activePerson != hit.transform.gameObject)
                        {
                            phoneContent.Repopulate(hit.transform.gameObject.GetComponent<Person>().posts);
                            gameplay.activePerson = hit.transform.gameObject;
                            gameplay.person = gameplay.activePerson.GetComponent<Person>();
                        }
                    }

                }
            }

        }
    }
}
