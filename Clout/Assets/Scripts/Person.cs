using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public bool follower;
    GameObject usernameCanvas;
    public float moveSpeed;
    public float closeMoveSpeed;
    static float maxX = 4.5f;
    static float maxZ = 4.5f;
    static float minX = -4.5f;
    static float minZ = -4.5f;
    public Vector3 targetPosition;
    public float rotateSpeed;
    public GameObject player;
    public float FOVRightAndLeft;

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
        else
        {
            if (!usernameCanvas.activeSelf) usernameCanvas.SetActive(true);
            FollowerWander();
        }
        ClampPositionAndRotation();
    }

    public void DisplayText()
    {
       
    }

    void PersonWander()
    {
        if (Vector3.Distance(targetPosition, transform.position) > .1f)
        {
            FaceAndMoveTo(targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            targetPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        }
    }

    void FollowerWander()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 currentPos = transform.position;
        float distanceFromPlayer = Vector3.Distance(playerPos, currentPos);
        if (distanceFromPlayer < 10 & distanceFromPlayer >= 3)
        {
            if (Vector3.Distance(targetPosition, transform.position) > .1f)
            {
                FaceAndMoveTo(targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                targetPosition = GetRandomPointInSegment(45);
            }
        }
        else if (distanceFromPlayer < 3)
        {
            Vector3 targetDir = playerPos - currentPos;
            //transform.position = Vector3.MoveTowards(currentPos, (new Vector3(targetDir.x, transform.position.y, targetDir.z)), -1 * closeMoveSpeed);
            FaceAndMoveTo(new Vector3(targetDir.x, transform.position.y, targetDir.z), -1 * closeMoveSpeed * Time.deltaTime);
        }
        else
        {
            PersonWander();
        }
    }

    void ClampPositionAndRotation()
    {
        Vector3 position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            0f,
            Mathf.Clamp(transform.position.z, minZ, maxZ));
        Vector3 euler = new Vector3(0f, transform.rotation.y, 0f);
        Quaternion rotation = Quaternion.Euler(euler);
        transform.SetPositionAndRotation(position, rotation);
    }

    void FaceAndMoveTo(Vector3 target, float travelSpeed)
    {
        Vector3 targetDir = target - transform.position;
        if (Vector3.Angle(targetDir, transform.forward) >= .1f)
        {
            Face(rotateSpeed * Time.deltaTime, targetDir);
        }
        else
        {
            float maxSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, travelSpeed);
        }
    }

    void Face(float maxRadians, Vector3 targetDir)
    { 
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, maxRadians, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    Vector3 GetRandomPointInSegment(float offset)
    {
        Vector3 playerPosition2D = new Vector3(player.transform.position.x, 0,
            player.transform.position.z);
        Vector3 position2D = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 forwardDirection = -1 * (playerPosition2D - position2D);
        float forwardAngle = Vector3.Angle(Vector3.forward, forwardDirection);
        float randomAngle = Random.Range(forwardAngle - offset, forwardAngle + offset);
        Ray ray = new Ray(transform.position,
            new Vector3(Mathf.Cos((forwardAngle + randomAngle) * Mathf.Deg2Rad),
            transform.position.y,
            Mathf.Sin((forwardAngle + randomAngle) * Mathf.Deg2Rad)));
        RaycastHit hit;
        int layerMask = 1 << 8;
        Physics.Raycast(ray, out hit, 10, layerMask, QueryTriggerInteraction.UseGlobal);
        float maxDistance = hit.distance;
        return ray.direction * Random.Range(0, maxDistance);
    }
    
}
