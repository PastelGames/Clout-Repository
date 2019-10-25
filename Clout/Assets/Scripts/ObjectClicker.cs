using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public GameObject gameplay;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null &
                    hit.transform.gameObject.tag == "Person")
                { 
                    if (hit.transform.gameObject.GetComponent<Person>().follower == false) {
                        hit.transform.gameObject.GetComponent<Person>().follower = true;
                        gameplay.GetComponent<Gameplay>().followers++;
                    }
                    gameplay.GetComponent<Gameplay>().SwitchCanvases();
                }
            }

        }
    }
}
