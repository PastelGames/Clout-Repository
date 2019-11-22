using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Wander : MonoBehaviour
{
    public float speed = 5;
    public float directionChangeInterval;
    public float maxHeadingChange = 180;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    bool moving = true;

    void Awake()
    {
        directionChangeInterval = Random.Range(1, 8f);
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        StartCoroutine(NewHeading());
        StartCoroutine(WanderWait());
    }

    void Update()
    { 
        if (moving == true)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
            var forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed);
            Vector3 clampedPosition = new Vector3(Mathf.Clamp(transform.position.x, -4, 4), transform.position.y, Mathf.Clamp(transform.position.z, -4, 4));
            transform.position = clampedPosition;
        }
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    IEnumerator WanderWait()
    {
        //Initial state is moving.
        //stop moving for x amount of seconds
        moving = false;
        yield return new WaitForSeconds(Random.Range(1, 5));
        moving = true;
        yield return new WaitForSeconds(Random.Range(6, 30));
        StartCoroutine(WanderWait());
        
    }
}
