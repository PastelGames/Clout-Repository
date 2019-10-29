using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public bool follower;
    GameObject usernameCanvas;
    public float moveSpeed;
    static float maxX = 4.5f;
    static float maxZ = 4.5f;
    static float minX = -4.5f;
    static float minZ = -4.5f;
    Vector3 targetPosition;
    public bool rotating;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        usernameCanvas = gameObject.transform.Find("Username Canvas").gameObject;
        targetPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {
        if (!follower)
        {
            PersonWander();
        }
        else if (!usernameCanvas.activeSelf)
        {
            usernameCanvas.SetActive(true);
        }
    }

    public void DisplayText()
    {
       
    }

    void PersonWander()
    {
        if (!rotating)
        {
            float maxSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxSpeed);
            transform.LookAt(targetPosition);
        }
        else
        {
            Vector3 targetDir = targetPosition - transform.position;
            float maxRadians = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            if (Vector3.Angle(targetDir, transform.forward) <= .1f)
            {
                rotating = false;
            }
        }
        if (Vector3.Distance(targetPosition, transform.position) < .001f)
        {
            targetPosition = targetPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
            rotating = true;
        }
    }


}
